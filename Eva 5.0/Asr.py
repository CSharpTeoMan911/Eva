from moonshine_voice import (
    LineCompleted,
    LineTextChanged,
    MicTranscriber,
    ModelArch,
    Transcriber,
    TranscriptEventListener,
)
import time
import os
import sys

BASE_DIR = os.path.dirname(os.path.abspath(__file__))


class Listener(TranscriptEventListener):
    def __init__(self, engine: "AsrEngine"):
        self.engine = engine

    def on_line_started(self, event):
       self.engine._processHypothesis(event.line.text)

    def on_line_text_changed(self, event: LineTextChanged) -> None:
        self.engine._processHypothesis(event.line.text)

    def on_line_completed(self, event: LineCompleted) -> None:
        self.engine._processLine(event.line.text)


class AsrEngine:
    keywords = str()
    engineLoaded = False
    transcriptionStarted = False

    transcriptionTimeout = float() 

    hypothesis = str()
    result = str()

    hypothesisTime = float()
    resultTime = float()

    def __init__(self, transcriptionTimeout:float, keywords:str=str()):
        self.keywords = keywords
        self.transcriptionTimeout = transcriptionTimeout if transcriptionTimeout >= 1 else 1
        self.transcriber = None
        self.mic = MicTranscriber()

    def _processHypothesis(self, text:str):
        if sys.getsizeof(self.hypothesis) >= 1024 * 1024 * 10: # 10 MB
            self.hypothesis = " ".join(self.hypothesis.split()[: len(self.hypothesis.split()) // 2])

        if text is not None and text != '':
            self.hypothesis = f'{self.hypothesis} {text}'.strip('\r').strip('\n')
            self.hypothesisTime = time.time()


    def _processLine(self, line:str):
        if sys.getsizeof(self.result) >= 1024 * 1024 * 10: # 10 MB
            self.result = " ".join(self.result.split()[: len(self.result.split()) // 2])

        if line is not None and line != '':
            self.result = f'{self.result} {line}'.strip('\r').strip('\n')
            self.resultTime = time.time()


    def loadEngine(self):
        transcriptionModel = os.path.join(BASE_DIR, 'medium-streaming-en', 'quantized_26_08_21')
        self.transcriber = Transcriber(
            model_path=transcriptionModel,
            model_arch=ModelArch.MEDIUM_STREAMING,
            update_interval=1,
            options={
                "context": self.keywords,
                "context_max_terms": 150,
                "keyterm_boost": 4.0,
                "vad_threshold": 0.05,
                "decode_incomplete_lines": True,
                "use_speculative_decoding": True,
            },
        )
        self.mic = (
        MicTranscriber()
        .use_transcriber(self.transcriber)
        .update_interval(1)
        .language("en")
        )
        self.mic.add_listener(Listener(self))
        self.mic.load()
        self.engineLoaded = True

    def unloadEngine(self):
        self.mic.close()
        if self.transcriber is not None:
            self.transcriber.close()
            self.transcriber = None
        self.engineLoaded = False


    def startTranscription(self):
        self.mic.start()
        self.transcriptionStarted = True

    def stopTranscription(self):
        self.mic.stop()
        self.transcriptionStarted = False

    def getTrasncriptionState(self) -> bool:
        return self.transcriptionStarted

    def getHypothesis(self) -> str:
        if self.engineLoaded is True:
            val = self.hypothesis
            self.hypothesisTime = self.hypothesisTime if self.hypothesisTime > 0 else time.time()
            if (time.time() - self.hypothesisTime) >= self.transcriptionTimeout:
                self.hypothesis = str()
            self.hypothesisTime = time.time()
            return val
        else:
            raise Exception("The ASR Engine is not loaded")


    def getResult(self) -> str:
        if self.engineLoaded is True:
            if self.result and self.resultTime > 0 and (time.time() - self.resultTime) >= self.transcriptionTimeout:
                val = self.result
                self.result = str()
                self.resultTime = 0
                return val
            return str()
        else:
            raise Exception("The ASR Engine is not loaded")

    def clearResult(self):
        self.result = str()
        self.resultTime = 0

    def clearHypothesis(self):
        self.hypothesis = str()