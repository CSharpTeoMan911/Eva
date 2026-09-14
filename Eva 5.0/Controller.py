import time

class Controller:
    import Asr

    hypothesis = str()
    result = str()

    isControllerRunning = False

    def __init__(self):
        self.engine = self.Asr.AsrEngine(transcriptionTimeout=3)

    def start(self):
        self.engine.loadEngine()
        self.engine.startTranscription()

    def stop(self):
        self.engine.unloadEngine()
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


controller = Controller()
controller.start()
print('[ loaded ]', flush=True)
t = time.time()
while True:
    if (time.time() - t) > 1:
        t = time.time()
        print(controller.getResult(), flush=True)

