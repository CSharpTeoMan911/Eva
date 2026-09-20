import time
import sys
import Asr
import traceback

class Controller:
    context = str()
    hypothesis = str()
    result = str()

    isControllerRunning = False

    def __init__(self, context:str = str()):
        self.context = context
        self.engine = Asr.AsrEngine(transcriptionTimeout=3, context=self.context)

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
key = str()
values = []
has_parameters = False

if len(sys.argv) >= 3:
    key = sys.argv[1]
    values = [x for x in sys.argv[2:]]
    has_parameters = True if key == '-c' and len(values) > 0 else False

values.extend(control_words)

controller = Controller() if has_parameters else Controller(context=str(values))
controller.start()

Loaded = False

t = time.time()
while True:
    if (time.time() - t) >= 1:
        if not Loaded:
            print('[ loaded ]', flush=True)
            Loaded = True
        else:
            print(controller.getResult(), flush=True)

        match input():
            case '[ startTranscription ]':
                controller.startTranscription()
            case '[ stopTranscription ]':
                controller.stopTranscription()
        t = time.time()


