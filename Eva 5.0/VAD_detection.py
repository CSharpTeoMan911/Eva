import numpy
import numpy as np
import torch
from silero_vad import load_silero_vad


class VAD:
    model = load_silero_vad()

    # Credit to Alexander Veysov
    async def int2float(self, sound: np.ndarray) -> np.ndarray:
        abs_max = numpy.abs(sound).max()
        sound = sound.astype('float32')
        if abs_max > 0:
            sound *= 1 / 32768
        sound = sound.squeeze()  # depends on the use case
        return sound

    async def audio_reader(self, data: bytes) -> bool:
        is_speech = False

        try:
            audio_int16 = numpy.frombuffer(data, numpy.int16)
            audio_float32 = await self.int2float(audio_int16)

            # get the confidence
            if self.model(torch.from_numpy(audio_float32), 16000).item() >= 0.6:
                is_speech = True
            self.model.reset_states()

        except Exception:
            pass

        return is_speech
