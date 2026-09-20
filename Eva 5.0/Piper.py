from piper import PiperVoice
import io
import threading
import time
import wave
import winsound

class Piper:
    voice = None
    processing = False
    lock = threading.Lock()
    
    def __init__(self, path:str=str()) -> None:
        self.voice = PiperVoice.load(path)

    def synthesys(self, text:str=str()) -> dict[float, bytes]:
        result = bytes()
        duration = 0

        if self.processing is False:
            if self.voice != None :
                sample_rate = self.voice.config.sample_rate
                sample_width = 2
                sample_channels = 1
                audio_data = bytearray()

                for chunk in self.voice.synthesize(text):
                    audio_data.extend(chunk.audio_int16_bytes)
                    sample_rate = chunk.sample_rate
                    sample_width = chunk.sample_width
                    sample_channels = chunk.sample_channels

                with io.BytesIO() as wav_buffer:
                    with wave.open(wav_buffer, "wb") as wav_file:
                        wav_file.setnchannels(sample_channels)
                        wav_file.setsampwidth(sample_width)
                        wav_file.setframerate(sample_rate)
                        wav_file.writeframes(audio_data)

                    duration = float(len(audio_data) / (sample_rate * sample_width * sample_channels)) + 0.05
                    result = wav_buffer.getvalue()
            else:
                raise Exception('[ Error ] - Piper is not initialised')
            
        return {duration:result}

    def __play_audio__(self, duration:float=float()):
        start_time = time.perf_counter()
        progress = float()
        while progress < 1:
            elapsed = min(time.perf_counter() - start_time, duration)
            progress = elapsed / duration
        return
        
        

    def play_audio(self, wav_bytes:bytes=bytes(), duration:float=float()) -> None:
        with self.lock:
            self.processing = True

        winsound.PlaySound(wav_bytes, winsound.SND_MEMORY)
        playback_thread = threading.Thread(target=self.__play_audio__, args=[duration])
        playback_thread.start()

        while playback_thread.is_alive():
            pass

        with self.lock:
            self.processing = False
        
        
