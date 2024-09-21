import hazelcast

if __name__ == "__main__":
    client = hazelcast.HazelcastClient()

    map = client.get_map("custom-configuration-map-hazelcast").blocking()

    map.put("custom.config", "Custom value")
    map.put("custom.config2", "Custom value 2")
    map.put("custom.config3", "Custom value 3")
    map.put("AppSettings:ApplicationName", "ApplicationNameFromHazelcast")

    print("Entradas adicionadas no map com sucesso.")

    client.shutdown()