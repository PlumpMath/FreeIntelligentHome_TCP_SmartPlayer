
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;


namespace TCP_SmartPlayer
{
    public class Key
    {
        private Dictionary<Keys, string> specialKeys = new Dictionary<Keys, string>();

        public Key()
        {
            specialKeys.Add(Keys.Back, "{BACKSPACE}"); // Rücktaste
            specialKeys.Add(Keys.Pause, "{BREAK}"); // Pausetaste / Untbr - Taste
            specialKeys.Add(Keys.CapsLock, "{CAPSLOCK} "); //Feststelltaste
            specialKeys.Add(Keys.Delete, "{DELETE}"); // Entf - Taste
            specialKeys.Add(Keys.End, "{END} "); // Ende - Taste
            specialKeys.Add(Keys.Enter, "{ENTER}"); // Eingabetaste
            specialKeys.Add(Keys.Escape, "{ESC}"); // ESC - Taste
            specialKeys.Add(Keys.Help, "{HELP}"); // Help - Taste
            specialKeys.Add(Keys.Home, "{HOME}"); // Windowns - Taste
            specialKeys.Add(Keys.Insert, "{INSERT}"); // Einfg - Taste
            specialKeys.Add(Keys.Left, "{LEFT}"); // Pfeil-Nach-Links - Taste
            specialKeys.Add(Keys.Right, "{RIGHT}"); // Pfeil-Nach-Rechts - Taste
            specialKeys.Add(Keys.Up, "{UP}"); // Pfeil-Nach-Oben - Taste
            specialKeys.Add(Keys.Down, "{DOWN} "); // Pfeil-Nach-Unten - Taste
            specialKeys.Add(Keys.NumLock, "{NUMLOCK}"); // Num - Taste
            specialKeys.Add(Keys.PageDown, "{PGDN}"); // Bild-Pfeil-Runter - Taste
            specialKeys.Add(Keys.PageUp, "{PGUP}"); // Bild-Pfeil-Rauf - Taste
            specialKeys.Add(Keys.Print, "{PRTSC}"); // Druck - Taste
            specialKeys.Add(Keys.Scroll, "{SCROLLLOCK}"); // Rollen - Taste
            specialKeys.Add(Keys.Tab, "{TAB}"); // Tabulator - Taste#
            specialKeys.Add(Keys.F1, "{F1}"); // F1 - Taste
            specialKeys.Add(Keys.F2, "{F2}"); // F2 - Taste
            specialKeys.Add(Keys.F3, "{F3}"); // F3 - Taste
            specialKeys.Add(Keys.F4, "{F4}"); // F4 - Taste
            specialKeys.Add(Keys.F5, "{F5}"); // F5 - Taste
            specialKeys.Add(Keys.F6, "{F6}"); // F6 - Taste
            specialKeys.Add(Keys.F7, "{F7}"); // F7 - Taste
            specialKeys.Add(Keys.F8, "{F8}"); // F8 - Taste
            specialKeys.Add(Keys.F9, "{F9}"); // F9 - Taste
            specialKeys.Add(Keys.F10, "{F10}"); // F10 - Taste
            specialKeys.Add(Keys.F11, "{F11}"); // F11 - Taste
            specialKeys.Add(Keys.F12, "{F12}"); // F12 - Taste
            specialKeys.Add(Keys.F13, "{F13}"); // F13 - Taste
            specialKeys.Add(Keys.F14, "{F14}"); // F14 - Taste
            specialKeys.Add(Keys.F15, "{F15}"); // F15 - Taste
            specialKeys.Add(Keys.F16, "{F16}"); // F16 - Taste
            specialKeys.Add(Keys.Add, "{ADD}"); // 'Plus' - Taste
            specialKeys.Add(Keys.Subtract, "{SUBTRACT}"); // 'Minus' - Taste
            specialKeys.Add(Keys.Multiply, "{MULTIPLY}"); // 'Mal' - Taste
            specialKeys.Add(Keys.Divide, "{DIVIDE}"); // 'Geteilt' - Taste
        }

        public void send(string theKey)
        {
            try
            {
                Keys key = (Keys)Enum.Parse(typeof(Keys), theKey, true);
                if (specialKeys.ContainsKey(key))
                {
                    SendKeys.Send(specialKeys[key]);
                    CmdConsole.instance().newMessages("Key gesendet: " + specialKeys[key].ToString());
                }
                else
                {
                    SendKeys.Send(theKey);
                    CmdConsole.instance().newMessages("Key gesendet: " + theKey.ToString());
                }
            }
            catch (Exception) { }
        }

        public void send(Keys taste)
        {
            if (specialKeys.ContainsKey(taste))
            {
                SendKeys.Send(specialKeys[taste]);
                CmdConsole.instance().newMessages("Key gesendet: " + specialKeys[taste].ToString());
            }
            else
            {
                string tmp = taste.ToString();
                SendKeys.Send(tmp);
                CmdConsole.instance().newMessages("Key gesendet: " + tmp);
            }
        }
    }
}