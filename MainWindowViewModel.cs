﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace AV_Player
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            OpenSettingsWindowCommand = new RelayCommand(OpenSettingsWindow);
            OpenFileCommand = new RelayCommand(OpenFile);
            PauseMediaCommand = new RelayCommand(PauseMedia);
            PlayMediaCommand = new RelayCommand(PlayMedia);
            RewindMediaCommand = new RelayCommand(RewindMedia);
            ForwardMediaCommand = new RelayCommand(ForwardMedia);
            LoadedMediaCommand = new RelayCommand(LoadedMedia);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Uri _mediaSource;
        public Uri MediaSource
        {
            get { return _mediaSource; }
            set
            {
                _mediaSource = value;
                NotifyPropertyChanged();
            }
        }

        private string _mediaFile;
        public string MediaFile
        {
            get { return _mediaFile; }
            set
            {
                _mediaFile = value;
                NotifyPropertyChanged();
            }
        }
        
        private ICommand _openSettingsWindowCommand;
        public ICommand OpenSettingsWindowCommand
        {
            get { return _openSettingsWindowCommand; }
            set { _openSettingsWindowCommand = value; }
        }

        private void OpenSettingsWindow(object obj)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.Show();
        }

        private ICommand _openFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _openFileCommand; }
            set { _openFileCommand = value; }
        }

        private void OpenFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg)|*.mp3;*.mpg;*.mpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                MediaSource = new Uri(openFileDialog.FileName);
                MediaFile = System.IO.Path.GetFileName(openFileDialog.FileName);
            }
        }

        private ICommand _playMediaCommand;
        public ICommand PlayMediaCommand
        {
            get { return _playMediaCommand; }
            set { _playMediaCommand = value; }
        }

        private void PlayMedia(object obj)
        {
            (obj as MediaElement).Play();
        }

        private ICommand _pauseMediaCommand;
        public ICommand PauseMediaCommand
        {
            get { return _pauseMediaCommand; }
            set { _pauseMediaCommand = value; }
        }

        private void PauseMedia(object obj)
        {
            (obj as MediaElement).Pause();
        }

        private ICommand _rewindMediaCommand;
        public ICommand RewindMediaCommand
        {
            get { return _rewindMediaCommand; }
            set { _rewindMediaCommand = value; }
        }

        private void RewindMedia(object obj)
        {
            (obj as MediaElement).Position = TimeSpan.FromSeconds((obj as MediaElement).Position.TotalSeconds - 10);
        }

        private ICommand _forwardMediaCommand;
        public ICommand ForwardMediaCommand
        {
            get { return _forwardMediaCommand; }
            set { _forwardMediaCommand = value; }
        }

        private void ForwardMedia(object obj)
        {
            (obj as MediaElement).Position = TimeSpan.FromSeconds((obj as MediaElement).Position.TotalSeconds + 10);
        }

        private ICommand _loadedMediaCommand;
        public ICommand LoadedMediaCommand
        {
            get { return _loadedMediaCommand; }
            set { _loadedMediaCommand = value; }
        }

        private void LoadedMedia(object obj)
        {
            if (MediaSource != null)
            {
                MediaFile = System.IO.Path.GetFileName(MediaSource.AbsolutePath);
            }
        } 
    }
}
