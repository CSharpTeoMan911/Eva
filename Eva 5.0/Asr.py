from moonshine_voice import MicTranscriber
from moonshine_voice.transcriber import MOONSHINE_FLAG_SPELLING_MODE
import time
import os
import sys

BASE_DIR = os.path.dirname(os.path.abspath(__file__))

class AsrEngine:
    context = str()
    engineLoaded = False
    transcriptionStarted = False

    transcriptionTimeout = int() 
    mic = MicTranscriber()

    hypothesis = str()
    result = str()

    hypothesisTime = float()
    resultTime = float()

    def __init__(self, transcriptionTimeout:int, context:str=str()):
        self.context = context
        self.transcriptionTimeout = transcriptionTimeout if transcriptionTimeout >= 3 else 3

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
        self.mic = (
        MicTranscriber()
        .options({
            "context_max_terms": 150,     # Limit context parsing to keep the model focused
            "keyterm_boost": 3.0,          # Boost specific phrases (default 2.0, max 4.0)
            "vad_threshold": 0.3,          # Raise from 0.5 to discard breathing or fan hum
            "max_tokens_per_second": 6.5,  # Ideal for structural/Latin languages like English
            "use_speculative_decoding": True
        })
        .models_from(transcriptionModel)
        .update_interval(1)
        .language("en")
        .on_text(lambda text: self._processHypothesis(text))
        .on_line(lambda line: self._processLine(line.text))
        )
        self.mic.load()
        self.engineLoaded = True

    def unloadEngine(self):
        self.mic.close()
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
            val = self.result
            self.resultTime = self.resultTime if self.resultTime > 0 else time.time()
            if (time.time() - self.resultTime) >= self.transcriptionTimeout:
                self.result = str()
                self.resultTime = time.time()
            return val
        else:
            raise Exception("The ASR Engine is not loaded")

    def clearResult(self):
        self.result = str()

    def clearHypothesis(self):
        self.hypothesis = str()