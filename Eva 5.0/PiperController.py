import os
import Piper
import pathlib

path =  os.path.join(pathlib.Path(__file__).parent.resolve(), 'eva_voice', 'en_GB-cori-high.onnx')
piper = Piper.Piper(path=path)

print("[ loaded ]", flush=True)

while True:
    t = input()
    s = piper.synthesys(text=t)
    k = list(s.keys())[0]
    piper.play_audio(s[k], k)
    print("[ Synthesis finished ]", flush=True)