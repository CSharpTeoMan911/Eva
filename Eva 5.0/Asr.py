from moonshine_voice import MicTranscriber
import time
import os

class AsrEngine:

    engineLoaded = False

    transcriptionTimeout = int() 
    mic = MicTranscriber()

    hypothesis = str()
    result = str()

    hypothesisTime = float()
    resultTime = float()

    def __init__(self, transcriptionTimeout:int):
        self.transcriptionTimeout = transcriptionTimeout if transcriptionTimeout >= 3 else 3

    def _processHypothesis(self, text:str):
        if text is not None and text != '':
            self.hypothesis = f'{self.hypothesis} {text}'.strip('\r').strip('\n')
            self.hypothesisTime = time.time()


    def _processLine(self, line:str):
        if line is not None and line != '':
            self.result = f'{self.result} {line}'.strip('\r').strip('\n')
            self.resultTime = time.time()


    def loadEngine(self):
        spellingModel = os.path.join(os.getcwd(), 'MoonshineEngine', 'spelling-en', 'spelling_cnn.ort')
        transcriptionModel = os.path.join(os.getcwd(), 'MoonshineEngine', 'medium-streaming-en' , 'quantized_26_08_21')
        self.mic = (
        MicTranscriber()
        .options({
            "spelling_model_path": spellingModel
        })
        .models_from(transcriptionModel)
        .update_interval(1.2)
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

    def stopTranscription(self):
        self.mic.stop()


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