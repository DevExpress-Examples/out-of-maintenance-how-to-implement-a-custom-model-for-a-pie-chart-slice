using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Charts;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfApplication2 {
    public class PieChartSelectionBehavior : Behavior<ChartControl> {
        public static readonly DependencyProperty ExpandAnimationProperty =
            DependencyProperty.Register("ExpandAnimation", typeof(DoubleAnimation), typeof(PieChartSelectionBehavior), new PropertyMetadata(null));
        public static readonly DependencyProperty CollapseAnimationProperty =
            DependencyProperty.Register("CollapseAnimation", typeof(DoubleAnimation), typeof(PieChartSelectionBehavior), new PropertyMetadata(null));

        const int clickDelta = 200;

        DateTime mouseDownTime;
        SeriesPoint currentExplodedPoint;

        ChartControl Chart { get { return AssociatedObject; } }

        public DoubleAnimation ExpandAnimation {
            get { return (DoubleAnimation)GetValue(ExpandAnimationProperty); }
            set { SetValue(ExpandAnimationProperty, value); }
        }
        public DoubleAnimation CollapseAnimation {
            get { return (DoubleAnimation)GetValue(CollapseAnimationProperty); }
            set { SetValue(CollapseAnimationProperty, value); }
        }

        void Chart_MouseDown(object sender, MouseButtonEventArgs e) {
            this.mouseDownTime = DateTime.Now;
        }
        void Chart_MouseUp(object sender, MouseButtonEventArgs e) {
            ChartHitInfo hitInfo = Chart.CalcHitInfo(e.GetPosition(Chart));
            if (hitInfo == null || hitInfo.SeriesPoint == null || !IsClick(DateTime.Now))
                return;
            Animate(hitInfo.SeriesPoint);
        }
        void Animate(SeriesPoint point) {
            double distance = PieSeries.GetExplodedDistance(point);
            Storyboard storyBoard = new Storyboard();
            var animation = distance > 0 ? CollapseAnimation : ExpandAnimation;
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, point);
            Storyboard.SetTargetProperty(animation, new PropertyPath(PieSeries.ExplodedDistanceProperty));
            if (distance == 0) {
                EventHandler onComplete = null;
                onComplete = (s, e) => {
                    storyBoard.Completed -= onComplete;
                    if (currentExplodedPoint != null) {
                        SeriesPoint p = currentExplodedPoint;
                        currentExplodedPoint = null;
                        Animate(p);
                    }
                    currentExplodedPoint = point;
                };
                storyBoard.Completed += onComplete;
            }
            else
                currentExplodedPoint = null;
            storyBoard.Begin();
        }
        bool IsClick(DateTime mouseUpTime) {
            return (mouseUpTime - this.mouseDownTime).TotalMilliseconds < clickDelta;
        }
        protected override void OnAttached() {
            base.OnAttached();
            Chart.MouseDown += Chart_MouseDown;
            Chart.MouseUp += Chart_MouseUp;
        }
    }
}