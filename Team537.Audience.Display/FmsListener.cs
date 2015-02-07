using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Team537.Audience.Display.DataModel;
using Team537.Audience.Display.ViewModel;

namespace Team537.Audience.Display
{
    public class FmsListener
    {
        private static bool isRunning;
        private CancellationTokenSource cancellationToken;
        private CommandHandler commandHandler;

        public void Start(MainWindowViewModel viewModel)
        {
            if (isRunning)
            {
                throw new Exception("Already running");
            }

            isRunning = true;

            cancellationToken = new CancellationTokenSource();
            commandHandler = new CommandHandler(viewModel);

            Task.Factory.StartNew(() =>
                {
                    var fmsEventListener = new TcpListener(IPAddress.Any, 8005);
                    fmsEventListener.Start();
                    while (!cancellationToken.Token.IsCancellationRequested)
                    {
                        Debug.WriteLine("Waiting for connection");
                        var client = fmsEventListener.AcceptTcpClient();
                        Task.Factory.StartNew(() => ConnectionProcesser(client, commandHandler), cancellationToken.Token);
                    }
                }, cancellationToken.Token);
        }

        public void Stop()
        {
            Debug.WriteLine("Stopping");
            cancellationToken.Cancel();
            isRunning = false;
            Debug.WriteLine("Stopped");
        }

        private static void ConnectionProcesser(TcpClient client, CommandHandler commandHandler)
        {
            // Do your work here
            Debug.WriteLine("Client connected");

            // Receive until client closes connection, indicated by 0 return value
            var streamReader = new StreamReader(client.GetStream());
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                Debug.WriteLine("Received: {0}{1}", line, "t");
                commandHandler.SendCommand(line);
            }

            Debug.WriteLine("Client disconnected");
        }

        private class CommandHandler
        {
            private object LockObject = new object();
            private MainWindowViewModel viewModel;

            public CommandHandler(MainWindowViewModel viewModel)
            {
                this.viewModel = viewModel;
            }

            public void SendCommand(string rawCommand)
            {
                var commandParts = rawCommand.Split(' ');
                
                var command = commandParts[0];
                var parameter = commandParts.Length > 1 ? commandParts[1] : String.Empty;

                int intParameter;
                Int32.TryParse(parameter, out intParameter);

                lock (this.LockObject)
                {
                    switch(command)
                    {
                        case "r1":
                            viewModel.Red1 = intParameter;
                            break;

                        case "r2":
                            viewModel.Red2 = intParameter;
                            break;

                        case "r3":
                            viewModel.Red3 = intParameter;
                            break;

                        case "rs":
                            viewModel.RedScore = intParameter;
                            break;

                        case "b1":
                            viewModel.Blue1 = intParameter;
                            break;

                        case "b2":
                            viewModel.Blue2 = intParameter;
                            break;

                        case "b3":
                            viewModel.Blue3 = intParameter;
                            break;

                        case "bs":
                            viewModel.BlueScore = intParameter;
                            break;

                        case "match":
                            viewModel.MatchNumber = intParameter;
                            break;

                        case "time":
                            viewModel.TimeLeft = intParameter;
                            
                            if (viewModel.TimeLeft == 30)
                            {
                                viewModel.PlaySound(SoundEffect.Warning1);
                            }

                            if (viewModel.TimeLeft == 10)
                            {
                                viewModel.PlaySound(SoundEffect.Warning2);
                            }

                            break;

                        case "total-time":
                            viewModel.TotalTime = intParameter;
                            break;

                        case "auto-start":
                            // play noise
                            viewModel.PlaySound(SoundEffect.StartAuto);
                            break;

                        case "auto-end":
                            // play noise
                            break;

                        case "w1":
                            // play noise
                            viewModel.PlaySound(SoundEffect.Warning1);
                            break;

                        case "w2":
                            // play noise
                            viewModel.PlaySound(SoundEffect.Warning2);
                            break;

                        case "tele-start":
                            // play noise
                            viewModel.PlaySound(SoundEffect.StartTele);
                            break;

                        case "tele-end":
                            // play noise
                            viewModel.PlaySound(SoundEffect.EndMatch);
                            break;

                        case "abort-match":
                            // play noise
                            viewModel.PlaySound(SoundEffect.CancelMatch);
                            break;
                    }
                }
            }
        }
    }
}
