using System;
using UnityEngine;
using Aws.GameLift.Realtime.Types;
using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.CognitoIdentity;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Zenject;

// This data structure is returned by the client service when a game match is found
[System.Serializable]
public class PlayerSessionObject
{
   public string PlayerSessionId;
   public string PlayerId;
   public string GameSessionId;
   public string FleetId;
   public string CreationTime;
   public string Status;
   public string IpAddress;
   public string Port;
}

public class GameSessionFirst
{
   private RealTimeClient _realTimeClient;

   private byte[] connectionPayload = new Byte[64];
   
   [Inject]
   public GameSessionFirst(RealTimeClient realTimeClient)
   {
      _realTimeClient = realTimeClient;
      setupMatch();
   }

   // make this more description to the player's action, like ballThrown
   public void playerAction(int opcode, string data)
   {
      Debug.Log("playerAction");
      Debug.Log(opcode);
      Debug.Log(data);
      _realTimeClient.SendMessage(DeliveryIntent.Fast, opcode, data);
   }

   async void setupMatch()
   {
      Debug.Log("setupMatch");

      CognitoAWSCredentials credentials = new CognitoAWSCredentials(
         "us-east-1:3eeac903-5836-4f03-be52-b6d19acdf90a", // Identity pool ID
         RegionEndpoint.USEast1 // Region
      );

      AmazonLambdaClient client = new AmazonLambdaClient(credentials, RegionEndpoint.USEast1);
      InvokeRequest request = new InvokeRequest
      {
         FunctionName = "TestLambda1",
         InvocationType = InvocationType.RequestResponse
      };

      Debug.Log("InvokeAsync");
      var response = await client.InvokeAsync(request);

      Debug.Log("after InvokeAsync");
      if (response.FunctionError == null)
      {
         if (response.StatusCode == 200)
         {
            var payload = Encoding.ASCII.GetString(response.Payload.ToArray()) + "\n";
            var playerSessionObj = JsonUtility.FromJson<PlayerSessionObject>(payload);

            Debug.Log(payload);

            if (playerSessionObj.FleetId == null)
            {
               Debug.Log($"Error in Lambda: {payload}");
            }
            else
            {
               joinMatch(playerSessionObj.IpAddress, playerSessionObj.Port, playerSessionObj.PlayerSessionId);
            }
         }
      }
      else
      {
         Debug.LogError(response.FunctionError);
      }
   }

   void joinMatch(string playerSessionDns, string playerSessionPort, string playerSessionId)
   {
      Debug.Log($"[client] Attempting to connect to server dns: {playerSessionDns} TCP port: {playerSessionPort} Player Session ID: {playerSessionId}");

      int localPort = GetAvailablePort();
      Debug.Log(localPort);
      _realTimeClient.init(playerSessionDns,
         Int32.Parse(playerSessionPort), localPort, ConnectionType.RT_OVER_WS_UDP_UNSECURED, playerSessionId, connectionPayload);
   }

   // code from 
   // - https://stackoverflow.com/a/49408267/1956540 with a change to UDP
   // - https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-create-a-socket
   private static readonly IPEndPoint DefaultLoopbackEndpoint = new IPEndPoint(IPAddress.Loopback, port: 0);

   public static int GetAvailablePort()
   {
      using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
      {
         socket.Bind(DefaultLoopbackEndpoint);
         return ((IPEndPoint)socket.LocalEndPoint).Port;
      }
   }

}