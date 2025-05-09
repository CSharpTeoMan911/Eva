import time

class Pipe:
    pipe_name = r"\\.\\pipe\\eva_offline_speech_recognition_engine"
    async def send_data(self, message):
        start = time.time()
        while time.time() - start < 10:
            try:
                with open(self.pipe_name, "w") as pipe:
                    pipe.write(message)
                    pipe.flush()
                    break
            except Exception as e:
                print(e)
                pass