import os
import time
import torch
from flask import Flask, jsonify
from transformers import pipeline

app = Flask(__name__)

@app.route('/transcribe', methods=['GET'])
def transcribe_audio():
    
    audio = r"C:\Users\bhave\Desktop\Bhaveen Clone\UnitedInUnity\Are You There\Assets\Scripts\RecordedAudio.wav"

    # Wait for the audio file to be present
    while not os.path.isfile(audio):
        time.sleep(1)  # Wait for 1 second before checking again
        

    transcribe = pipeline(
        task = "automatic-speech-recognition",
        model = "vasista22/whisper-hindi-small",
        chunk_length_s = 30,
    )
   
    
    transcribe.model.config.forced_decoder_ids = (
        transcribe.tokenizer.get_decoder_prompt_ids(language = "hi", task="transcribe")
    )

    transcription = transcribe(audio)["text"]
    try:
        os.remove(audio)
    except:
        print("Error while deleting the audio file")
    
    

    return jsonify({'Transcription': transcription})


if __name__ == '__main__':
    app.run(host='<ip address>',port=5000, debug=True)
