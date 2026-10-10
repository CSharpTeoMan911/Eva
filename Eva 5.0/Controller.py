import time
import Asr
import subprocess
import sys

class Controller:
    keywords = list()
    hypothesis = str()
    result = str()

    isControllerRunning = False

    def __init__(self, keywords:str = str()):
        self.keywords = keywords
        self.engine = Asr.AsrEngine(transcriptionTimeout=1.5, keywords=self.keywords)

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
        h = str(self.engine.getHypothesis()).strip()

        if not h:
            return str()

        if not self.hypothesis:
            self.hypothesis = h
            return h

        if h == self.hypothesis:
            return str()

        prev_words = self.hypothesis.split()
        curr_words = h.split()

        i = 0
        while i < len(prev_words) and i < len(curr_words) and prev_words[i] == curr_words[i]:
            i += 1

        delta = ' '.join(curr_words[i:]).strip()
        self.hypothesis = h
        return delta
    
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

    if subprocess.getoutput('powershell -Command "(Get-Process \'Eva 5.0\' -ErrorAction SilentlyContinue).Id"') == "":
        sys.exit(0)

    if (time.time() - t) >= 1:
        if not Loaded:
            print('[Result: [ loaded ]]', flush=True)
            Loaded = True
        else:
            res = controller.getResult()
            hyp = controller.getHypothesis()

            if res != "":
                print(f'[Result: {res}]', flush=True)
            elif hyp != "":
                print(f'[Hypothesis: {hyp}]', flush=True)
                    
        t = time.time()

