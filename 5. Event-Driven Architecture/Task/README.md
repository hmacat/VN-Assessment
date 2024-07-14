To execute the RabbitMQ, you can use the following docker command:
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management

You can start the applications in any order. 
User registration service will generate UserRegistered events and send a message to RabbitMQ every 5 seconds. 
Mail notification service will consume the message in the queue to send email.
Both sent and received messages should be printed at the application consoles.
