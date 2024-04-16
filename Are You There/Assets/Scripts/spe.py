import torch
from transformers import pipeline

audio = r"C:\Users\bhave\Desktop\Bhaveen Clone\UnitedInUnity\Are You There\Assets\Scripts\WhatsApp Audio 2024-03-10 at 6.17.47 AM.wav"

transcribe = pipeline(
    task = "automatic-speech-recognition",
    model = "vasista22/whisper-hindi-small",
    chunk_length_s = 30,
)

transcribe.model.config.forced_decoder_ids = (
    transcribe.tokenizer.get_decoder_prompt_ids(language = "hi", task="transcribe")
)

print("Transciption" , transcribe(audio)["text"])

