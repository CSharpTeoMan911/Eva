import time
import sys
import Asr

class Controller:
    context = str()
    hypothesis = str()
    result = str()

    isControllerRunning = False

    def __init__(self, context:str = str()):
        self.engine = Asr.AsrEngine(transcriptionTimeout=3, context=self.context)
        self.context = context

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



key = str()
values = []
has_parameters = False

if len(sys.argv) >= 3:
    key = sys.argv[1]
    values = [x for x in sys.argv[2:]]
    has_parameters = True if key == '-c' and len(values) > 0 else False

controller = Controller() if has_parameters else Controller(context=str(values))
controller.start()

print('[ loaded ]', flush=True)
t = time.time()
while True:
    if (time.time() - t) >= 1:
        t = time.time()
        print(controller.getResult(), flush=True)

