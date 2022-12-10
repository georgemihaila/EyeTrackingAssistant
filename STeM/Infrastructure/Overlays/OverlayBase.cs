using STeM.Infrastructure.Extensions;
using STeM.Infrastructure.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace STeM.Infrastructure.Overlays
{
    public abstract class OverlayBase : UIElement, IFocusable, IOverlay
    {
        public bool Enabled { get; set; } = true;
        protected IEnumerable<UIElement> UIElements => _addedUIElementsDictionary.Keys;
        protected TimeSpan LastUpdateDuration { get; private set; }
        private readonly Stopwatch _performanceStopwatch = new Stopwatch();
        private Dictionary<UIElement, bool> _addedUIElementsDictionary = new Dictionary<UIElement, bool>();
        protected List<TimedVector2> _lastIntervalEyeGazePositions { get; private set; } = new List<TimedVector2>();
        private readonly int _gazeLengthMilliseconds = 400;
        private bool _isFocused = false;
        private Vector2 _lastFocusPosition = Vector2.Zero;
        private void AddToCanvas(UIElement element, Canvas parent)
        {
            RegisterElementsIfNeeded(element);
            parent.Children.Add(element);
            _addedUIElementsDictionary[element] = true;
        }
        /// <summary>
        /// If required, adds a new UIElement to the parent(s) before an update is done
        /// </summary>
        protected void RegisterElementsIfNeeded(params UIElement[] elements)
        {
            foreach (var element in elements)
            {
                if (!_addedUIElementsDictionary.ContainsKey(element))
                    _addedUIElementsDictionary.Add(element, false);
            }
        }

        private void AddNewUIElements(Canvas parent)
        {
            foreach (var key in _addedUIElementsDictionary.Keys.ToList())
            {
                if (!_addedUIElementsDictionary[key])
                {
                    AddToCanvas(key, parent);
                }
            }
        }
        public void Update(Canvas parent)
        {
            _performanceStopwatch.Restart();
            if (Enabled)
            {
                OnUpdateInternal();
                AddNewUIElements(parent);

                if (_lastIntervalEyeGazePositions.Select(x => x.Vector2).IsWhiteNoise())
                {
                    if (!_isFocused)
                    {
                        _isFocused = true;
                        _lastFocusPosition = _lastIntervalEyeGazePositions.GetAverage();
                        OnFocus(_lastFocusPosition);
                    }
                }
                else if (_lastIntervalEyeGazePositions.GetVariance().LengthSquared() > 10)
                {
                    if (_isFocused)
                    {
                        _isFocused = false;
                        OnDefocus(_lastFocusPosition);
                    }
                }
            }
            _performanceStopwatch.Stop();
            LastUpdateDuration = _performanceStopwatch.Elapsed;
        }

        protected virtual void OnUpdateInternal() { }
        protected virtual void OnFocus(Vector2 position) { }
        protected virtual void OnDefocus(Vector2 position) { }
        protected virtual void OnEyePositionChangedInternal(Vector2 position) { }
        public void OnEyePositionChanged(Vector2 position)
        {
            _lastIntervalEyeGazePositions.RemoveAll(x => DateTime.Now.Subtract(x.Time).TotalMilliseconds > _gazeLengthMilliseconds);
            _lastIntervalEyeGazePositions.Add(new TimedVector2(DateTime.Now, position));
            OnEyePositionChangedInternal(position);
        }

        public OverlayBase() { }
        public OverlayBase(bool enabled)
        {
            Enabled = enabled;
        }
    }
}