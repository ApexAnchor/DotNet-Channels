# DotNet-Channels
A simple producer consumer application to demonstrate Dot net channels usage 

# Producer-Consumer Application with .NET Channels

## Introduction

When building applications, especially those requiring long-running background tasks, the producer/consumer pattern becomes essential. .NET Channels offer a powerful solution for implementing this pattern without the complexity of a message broker like RabbitMQ. In this guide, we'll explore what Channels are and demonstrate how to create a simple producer-consumer application using Channels.

## What are Channels?

Channels are part of the .NET Core base library, eliminating the need for additional package dependencies. A Channel is a data structure designed to store data from a producer, which can then be consumed by one or more consumers. It's important to understand that Channels differ from a Publisher/Subscriber (Pub/Sub) model. In Channels, only one consumer can read a given message, unlike Pub/Sub, where a message can be received by multiple subscribers concurrently.
There are 2 types of channels
# Unbounded Channel  
When you create an unbounded channel, by default, the channel can be used by any number of readers and writers concurrently.
# Bounded Channel
When you create a bounded channel, the channel is bound to a maximum capacity. When the bound is reached, the default behavior is that the channel asynchronously blocks the producer until space becomes available.

## Getting Started

To illustrate the usage of Channels, let's create a simple console application.

## Creating an Unbounded Channel

For our example, we'll use an unbounded channel, eliminating concerns about capacity or other constraints.

```csharp

    static async Task ProduceDataAsync(ChannelWriter<string> writer)
    {
        for (int i = 0; i < 5; i++)
        {
            // Produce data and write to the channel
            await writer.WriteAsync($"Message {i}");
            Console.WriteLine($"Produced: Message {i}");

            // Simulate some processing time
            await Task.Delay(1000);
        }

        // Complete the writer to signal the end of data production
        writer.Complete();
    }

    static async Task ConsumeDataAsync(ChannelReader<string> reader)
    {
        // Read data until the channel is completed
        await foreach (var message in reader.ReadAllAsync())
        {
            // Consume the data
            Console.WriteLine($"Consumed: {message}");

            // Simulate some processing time
            await Task.Delay(500);
        }
    }
}
```

The example demonstrates a simple application with a producer and a consumer using an unbounded channel. The consumer is run as background service waiting for the messages to be published to channel.
