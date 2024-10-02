from flask import Flask, request, jsonify
import whisper
import os

app = Flask(__name__)


# Transcription function
def transcribe_audio(file_path):
    model = whisper.load_model("base")
    result = model.transcribe(file_path)
    return result["text"]


@app.route("/transcribe", methods=["POST"])
def transcribe():
    if "audio" not in request.files:
        return jsonify({"error": "No audio file provided"}), 400

    audio_file = request.files["audio"]
    temp_path = "temp_audio.wav"

    # Save the byte array as a temporary audio file
    audio_file.save(temp_path)

    try:
        # Transcribe the audio
        transcript = transcribe_audio(temp_path)
        print(f"Transcription: {transcript}")
        return jsonify({"transcription": transcript})
    except Exception as e:
        print(f"Error during transcription: {e}")
        return jsonify({"error": "Transcription failed"}), 500
    finally:
        # Clean up the temporary file
        os.remove(temp_path)


if __name__ == "__main__":
    app.run(debug=True)
