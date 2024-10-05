from flask import Flask, request, jsonify
import whisper
import os

app = Flask(__name__)
model = whisper.load_model("base")
print("Model loaded successfully")


# Transcription function
def transcribe_audio(file_path):
    result = model.transcribe(file_path)
    return result["text"]


@app.route("/transcribe", methods=["POST"])
def transcribe():
    print("Transcription request received")
    if "audio" not in request.files:
        return jsonify({"error": "No audio file provided"}), 400

    audio_file = request.files["audio"]
    temp_path = "/app/audio_files/temp_audio.wav"

    # Save the byte array as a temporary audio file
    audio_file.save(temp_path)

    try:
        # Transcribe the audio
        transcript = transcribe_audio(temp_path)
        print(f"Transcription: {transcript}")
        return jsonify({"transcription": transcript})
    except Exception as e:
        print(f"Error during transcription: {e}")
        return jsonify({"error": f"Transcription failed. {e}"}), 500
    finally:
        # Clean up the temporary file
        os.remove(temp_path)


if __name__ == "__main__":
    print("Starting server on http://transcription-server:5000")
    app.run(host="0.0.0.0", port=5000, debug=True)