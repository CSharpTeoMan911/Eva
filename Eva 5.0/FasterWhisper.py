import os
import numpy as np
from faster_whisper import WhisperModel

class Whisper:
    model_path= os.path.join(os.getcwd(), "faster-whisper-small")
    model = WhisperModel(model_path, device="cpu", compute_type="int8")

    async def transcription(self, data: bytes) -> str:
        text = str()

        formatted_data = np.frombuffer(data, np.int16).astype(np.float32) / 255.0
        segments, info = self.model.transcribe(formatted_data, beam_size=5)
        segments = list(segments)

        for segment in segments:
            text += segment.text

        return text