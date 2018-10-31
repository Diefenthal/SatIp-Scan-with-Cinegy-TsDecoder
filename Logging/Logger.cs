/* Copyright 2018 Kay Diefenthal.

  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
*/

using System;
using System.Diagnostics;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4Hierarchy = log4net.Repository.Hierarchy;

namespace SatIp
{
    public static class Logger
    {
        private const string Path = "SatIp ";
        private static ILog _log;

        private static SourceLevels _sourceLevels = SourceLevels.Off;

        public static void ConfigureNHibernateLoggers()
        {
#if DEBUG
            LogManager.GetRepository().Threshold = Level.Warn;
#else
            LogManager.GetRepository().Threshold = Level.Error;
#endif
        }

        /// <summary>
        ///     Warning: this is not tread-safe, so only call this at startup or at a time that you are sure your
        ///     process is not performing any logging!
        /// </summary>
        /// <param name="filePath">The path to the log file.</param>
        /// <param name="sourceLevels">The lowest log level to log.</param>
        public static void SetLogFilePath(string filePath, SourceLevels sourceLevels)
        {
            if (!System.IO.Path.IsPathRooted(filePath))
                filePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    System.IO.Path.Combine(@"SatIp RtspSample\Logs", filePath));

            var level = Level.Debug;
            if (_sourceLevels == SourceLevels.Information)
                level = Level.Info;
            else if (_sourceLevels == SourceLevels.Warning)
                level = Level.Warn;
            else if (_sourceLevels == SourceLevels.Error)
                level = Level.Error;
            else if (_sourceLevels == SourceLevels.Critical) level = Level.Fatal;
            _sourceLevels = sourceLevels;

            var hierarchy =
                (log4Hierarchy.Hierarchy) LogManager.GetAllRepositories().FirstOrDefault(r => r.Name == Path) ??
                (log4Hierarchy.Hierarchy) LogManager.CreateRepository(Path);
            hierarchy.Root.RemoveAllAppenders();

            var roller = new RollingFileAppender();
            var patternLayout = new PatternLayout {ConversionPattern = "%date [%-5level][%thread]: %message%newline"};
            patternLayout.ActivateOptions();
            roller.Layout = patternLayout;
            roller.AppendToFile = true;
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.MaxSizeRollBackups = 4;
            roller.MaximumFileSize = "1000KB";
            roller.StaticLogFileName = true;
            roller.File = filePath;
            roller.ActivateOptions();
            roller.AddFilter(new LevelRangeFilter
            {
                LevelMin = level,
                LevelMax = Level.Fatal
            });
            BasicConfigurator.Configure(hierarchy, roller);

            var coreLogger = hierarchy.GetLogger(Path) as log4Hierarchy.Logger;
            if (coreLogger != null) coreLogger.Level = level;

            _log = LogManager.GetLogger(hierarchy.Name, Path);
        }

        public static void Write(string message, params object[] args)
        {
            if (_log.IsInfoEnabled) _log.InfoFormat(message, args);
        }

        public static void Info(string message, params object[] args)
        {
            if (_log.IsInfoEnabled) _log.InfoFormat(message, args);
        }

        public static void Warn(string message, params object[] args)
        {
            if (_log.IsWarnEnabled) _log.WarnFormat(message, args);
        }

        public static void Error(string message, params object[] args)
        {
            if (_log.IsErrorEnabled) _log.ErrorFormat(message, args);
        }

        public static void Critical(string message, params object[] args)
        {
            if (_log.IsFatalEnabled) _log.FatalFormat(message, args);
        }

        public static void Verbose(string message, params object[] args)
        {
            if (_log.IsDebugEnabled) _log.DebugFormat(message, args);
        }

        public static void Write(TraceEventType severity, string message, params object[] args)
        {
            Write(null, severity, message, args);
        }

        public static void Write(string category, TraceEventType severity, string message, params object[] args)
        {
            switch (severity)
            {
                case TraceEventType.Verbose:
                    Verbose(message, args);
                    break;
                case TraceEventType.Information:
                    Info(message, args);
                    break;
                case TraceEventType.Warning:
                    Warn(message, args);
                    break;
                case TraceEventType.Error:
                    Error(message, args);
                    break;
                case TraceEventType.Critical:
                    Critical(message, args);
                    break;
            }
        }

        #region Logging Enabled?

        public static bool IsVerboseEnabled => _sourceLevels >= SourceLevels.Verbose;

        public static bool IsInformationEnabled => _sourceLevels >= SourceLevels.Information;

        public static bool IsWarningEnabled => _sourceLevels >= SourceLevels.Warning;

        public static bool IsErrorEnabled => _sourceLevels >= SourceLevels.Error;

        public static bool IsCriticalEnabled => _sourceLevels >= SourceLevels.Critical;

        #endregion
    }
}