from flask import Flask, request, jsonify
import hazelcast
import json

app = Flask(__name__)

client = hazelcast.HazelcastClient()
map = client.get_map("WeatherApi").blocking()
topic = client.get_topic("configuration.WeatherApi")

@app.route('/add-entry', methods=['POST'])
def add_entry():
    try:
        data = request.json
        key = data.get("key")
        value = data.get("value")
        
        if not key or not value:
            return jsonify({"error": "Chave ou valor ausente"}), 400
        
        map.put(key, value)
        message = {
            "key": key,
            "value": value
        }
        topic.publish(json.dumps(message))

        return jsonify({"message": f"Entrada adicionada: {key} -> {value}"}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500

@app.route('/list-entries', methods=['GET'])
def list_entries():
    try:
        entries = map.entry_set()
        return jsonify({"entries": dict(entries)}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)