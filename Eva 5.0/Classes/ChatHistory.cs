using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Eva_5._0
{
    internal class ChatHistory
    {
        private readonly string chatsFileName = "ChatHistory.json";
        private readonly string chatsFilePath;
        private ConcurrentDictionary<long, ChatPackage> chatHistory = new ConcurrentDictionary<long, ChatPackage>();

        public ChatHistory()
        {
            chatsFilePath = new StringBuilder(Environment.CurrentDirectory).Append("\\").Append(chatsFileName).ToString();
            LoadChats();
        }

        public class ChatPackage
        {
            public string chatTitle { get; set; }
            public List<ChatGPT_API.messages> chat { get; set; }
        }

        public ConcurrentDictionary<long, ChatPackage> GetChats() => chatHistory;

        public ChatPackage GetChat(long id)
        {
            chatHistory.TryGetValue(id, out ChatPackage chat);
            return chat;
        }

        private async void LoadChats()
        {
            string json = await ReadFile();
            ConcurrentDictionary<long, ChatPackage> chats = JsonSerialisation.JsonDeserialiser<ConcurrentDictionary<long, ChatPackage>>(json);
            if (chats != null)
            {
                chatHistory = chats;
            }
        }
   
        public async Task UpdateChats(long id, ChatPackage chat)
        {
            if (chatHistory.TryGetValue(id, out ChatPackage value))
            {
                chatHistory[id] = value;
            }
            else
            {
                chatHistory.TryAdd(id, chat);
            }

            await WriteToFile();
        }

        public async Task RemoveChat(long id)
        {
            chatHistory.TryRemove(id, out _);
            await WriteToFile();
        }

        public int GetChatCount() => chatHistory.Count;

        // THIS METHOD ENSURES THAT THE PERMISSIONS TO THE SETTINGS FILE FOR THE CURRENT
        // USER INCLUDE READ, WRITE, AND DELETE FUNCTIONALITIES TO THE SETTINGS FILE.
        // THE SETTINGS FILE MUST HAVE READ AND WRITE PERMISSIONS IN ORDER FOR THE 
        // APPLICATION TO ACCESS THE CHATGPT API KEY AND THE SET VOLUME SETTINGS,
        // OTHERWISE THE APPLICATION CAN CRASH.
        private void Ensure_Access_To_The_Settings_File()
        {
            if (System.IO.File.Exists(chatsFilePath))
            {
                System.Security.AccessControl.FileSecurity settings_file_security = System.IO.File.GetAccessControl(chatsFilePath);

                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Write, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Read, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.Delete, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.WriteData, System.Security.AccessControl.AccessControlType.Allow));
                settings_file_security.AddAccessRule(new System.Security.AccessControl.FileSystemAccessRule(System.Security.Principal.WindowsIdentity.GetCurrent().Name, System.Security.AccessControl.FileSystemRights.ReadData, System.Security.AccessControl.AccessControlType.Allow));

                System.IO.File.SetAccessControl(chatsFilePath, settings_file_security);
            }
        }

        private async Task WriteToFile()
        {
            if (File.Exists(chatsFilePath))
            {
                Ensure_Access_To_The_Settings_File();
            }

            using (FileStream fs = File.Open(chatsFilePath, FileMode.Create))
            {
                string json = await JsonSerialisation.JsonSerialiser(chatHistory);
                byte[] json_binary = Encoding.UTF8.GetBytes(json);
                await fs.WriteAsync(json_binary, 0, json_binary.Length);
                await fs.FlushAsync();
            }
        }

        private async Task<string> ReadFile()
        {
            if (File.Exists(chatsFilePath))
            {
                Ensure_Access_To_The_Settings_File();

                using (FileStream fs = File.Open(chatsFilePath, FileMode.Open))
                {
                    byte[] buffer = new byte[fs.Length];
                    await fs.ReadAsync(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(buffer);
                }
            }

            return String.Empty;
        }
    }
}
