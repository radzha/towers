using System;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Вспомогательный класс для дебага.
/// </summary>
public static class LogExtensions
{
    /// <summary>
    /// Prints log.
    /// </summary>
    public static void Log(this object text, object addition = null)
    {
        addition = addition == null ? "" : ": " + addition;
        Debug.Log(text + addition.ToString());
    }

    /// <summary>
    /// Prints log.
    /// </summary>
    public static void LogEr(this object text, object addition = null)
    {
        addition = addition == null ? "" : ": " + addition;
        Debug.LogError(text + addition.ToString());
    }

    /// <summary>
    /// Prints log.
    /// </summary>
    public static void LogEx(this Exception e)
    {
        Debug.LogException(e);
    }

    /// <summary>
    /// Prints log in green.
    /// </summary>
    public static void CLog(this object text, object addition = null)
    {
        ColorLog(text, "green", addition);
    }

    /// <summary>
    /// Prints log in red.
    /// </summary>
    public static void CLogRed(this object text, object addition = null)
    {
        ColorLog(text, "red", addition);
    }

    /// <summary>
    /// Prints log in blue.
    /// </summary>
    public static void CLogBlue(this object text, object addition = null)
    {
        ColorLog(text, "blue", addition);
    }

    /// <summary>
    /// Prints log formatted.
    /// </summary>
    public static void LogF(this object text, params object[] vars)
    {
        if (string.IsNullOrEmpty((string) text))
        {
            Debug.Log(string.Join(", ", vars.Select(s => s.ToString()).ToArray()));
        }
        else
        {
            var builder = new StringBuilder();
            var tokens = ((string) text).Split(',');
            for (var i = 0; i < tokens.Length; i++)
            {
                var delimeter = i < tokens.Length - 1 ? ", " : "";
                builder.AppendFormat("{0}: {1}{2}", tokens[i], i < vars.Length ? vars[i] : "-", delimeter);
            }

            builder.ToString().Log();
        }
    }

    /// <summary>
    /// Prints colorized in green formatted log.
    /// </summary>
    public static void CLogF(this object text, params object[] vars)
    {
        ColorLogF(text, "green", vars);
    }

    /// <summary>
    /// Prints colorized in green formatted log.
    /// </summary>
    public static void CLogBlueF(this object text, params object[] vars)
    {
        ColorLogF(text, "blue", vars);
    }

    /// <summary>
    /// Prints colorized in green formatted log.
    /// </summary>
    public static void CLogRedF(this object text, params object[] vars)
    {
        ColorLogF(text, "red", vars);
    }

    /// <summary>
    /// Prints color log formatted.
    /// </summary>
    private static void ColorLogF(object text, string color, params object[] vars)
    {
        if (string.IsNullOrEmpty((string) text))
        {
            Debug.Log(string.Join(", ", vars.Select(s => s.ToString()).ToArray()));
        }
        else
        {
            var builder = new StringBuilder("$");
            var tokens = ((string) text).Split(',');
            for (var i = 0; i < tokens.Length; i++)
            {
                var delimeter = i < tokens.Length - 1 ? ", " : "";
                builder.AppendFormat("<color={0}><b>{1}</b></color>: <b>{2}</b>{3}", color, tokens[i],
                    i < vars.Length ? vars[i] : "-", delimeter);
            }

            builder.ToString().Log();
        }
    }

    /// <summary>
    /// Prints colorized log.
    /// </summary>
    private static void ColorLog(object text, string color, object addition = null)
    {
        addition = addition == null ? "" : ": " + addition;
        Debug.Log("$<color=" + color + "><b>" + text + "</b></color>" + addition);
    }
}