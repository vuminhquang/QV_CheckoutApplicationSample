#Run Zookeeper
D:\kafka_2.13-3.2.3\bin\windows\zookeeper-server-start.bat D:\kafka_2.13-3.2.3\config\zookeeper.properties 

#Run Kafka
D:\kafka_2.13-3.2.3\bin\windows\kafka-server-start.bat D:\kafka_2.13-3.2.3\config\server.properties   

#Create the topic
D:\kafka_2.13-3.2.3\bin\windows\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic CommandTopic