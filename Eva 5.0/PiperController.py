import os
import Piper
import pathlib
import subprocess
import threading

def process_checkup(shutdown_event: threading.Event):
    while not shutdown_event.is_set():
        if subprocess.getoutput('powershell -Command "(Get-Process \'Eva 5.0\' -ErrorAction SilentlyContinue).Id"') == "":
            shutdown_event.set()
            return
        shutdown_event.wait(1)
        
shutdown_event = threading.Event()
checkup_thread = threading.Thread(
    target=process_checkup,
    args=(shutdown_event,),
    daemon=True,
)
checkup_thread.start()

path =  os.path.join(pathlib.Path(__file__).parent.resolve(), 'eva_voice', 'en_GB-cori-high.onnx')
piper = Piper.Piper(path=path)

print("[ loaded ]", flush=True)


while True:
    t = input()
    s = piper.synthesys(text=t)
    k = list(s.keys())[0]
    piper.play_audio(s[k], k)
    print("[ Synthesis finished ]", flush=True)