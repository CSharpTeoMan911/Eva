import time
import sys
import Asr
import traceback

class Controller:
    keywords = list()
    hypothesis = str()
    result = str()

    isControllerRunning = False

    def __init__(self, keywords:str = str()):
        self.keywords = keywords
        self.engine = Asr.AsrEngine(transcriptionTimeout=3, keywords=self.keywords)

    def start(self):
        self.engine.loadEngine()
        self.engine.startTranscription()

    def stop(self):
        self.engine.unloadEngine()
        self.engine.stopTranscription()

    def startTranscription(self):
        if not self.engine.getTrasncriptionState():
            self.engine.startTranscription()

    def stopTranscription(self):
        if self.engine.getTrasncriptionState():
            self.engine.stopTranscription()

    def getResult(self) -> str:
        r = self.engine.getResult()
        val = r if self.result != r else str()
        self.result = r
        return val

    def getHypothesis(self) -> str:
        h = self.engine.getHypothesis()
        val = h if self.hypothesis != h else str()
        self.hypothesis = h
        return val
    
    def clearResult(self):
        self.engine.clearResult()

    def clearHypothesis(self):
        self.engine.clearHypothesis()


control_words = ["open",
            "close",
            "set",
            "search",
            "activate",
            "deactivate",
            "enable",
            "disable",
            "invisible",
            "visible",
            "take",
            "on",
            "a",
            "an",
            "please",
            "timer",
            "gpt",
            "screenshot",
            "mode"]

values = ','.join(control_words)

controller = Controller(keywords=values)
controller.start()
Loaded = False

t = time.time()
while True:
    if (time.time() - t) >= 1:
        if not Loaded:
            print('[Result: [ loaded ]]', flush=True)
            Loaded = True
        else:
            res = f'[Result: {controller.getResult()}]'
            hyp = f'[Result: {controller.getHypothesis()}]'

            if res is not None:
                print(res, flush=True)
            elif hyp is not None:
                print(hyp, flush=True)
        t = time.time()


