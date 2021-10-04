using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;

namespace Pairs.Effects
{
    public class ParticleEffect : PlatformEffect
    {
        public int NumberOfParticles { get; set; } = 100;
        public float LifeTime { get; set; } = 1.5f;
        public float Speed { get; set; } = 0.1f;
        public float Scale { get; set; } = 1.0f;
        public string Image { get; set; }

        public ParticleEffect() : base() {}

        protected override void OnAttached()
        {
            //var effect = Element.Effects.OfType<Pairs.Effects.ParticleEffect>().FirstOrDefault();

            //if (effect != null)
            //{
            //    particleEffect = effect;
            //    effect.Emit += OnEffectEmit;
            //}
            this.Emit += OnEffectEmit;
        }

        protected override void OnDetached()
        {
            this.Emit -= OnEffectEmit;
        }

        public event EventHandler<EventArgs> Emit;

        public void RaiseEmit() => Emit?.Invoke(this, EventArgs.Empty);

        private void OnEffectEmit(object sender, EventArgs e)
        {
            #if ANDROID
            var control = Control ?? Container;


            //long lifeTime = (long)(LifeTime * 1000 ?? 1500);
            //int numberOfItems = this.NumberOfParticles ?? 4000;
            //var scale = this.Scale ?? 1.0f;
            //var speed = this.Speed ?? 0.1f;
            //var image = this.Image ?? "ic_launcher";

            var location = new int[2];
            control.GetLocationOnScreen(location);

            var ctx = Microsoft.Maui.Essentials.Platform.CurrentActivity;
            var drawableImage = AndroidX.Core.Content.ContextCompat.GetDrawable(ctx, ctx.Resources.GetIdentifier(Image, "drawable", ctx.PackageName));
            var particleSystem = new Com.Plattysoft.Leonids.ParticleSystem(ctx, NumberOfParticles, drawableImage, (long)LifeTime);
            particleSystem
              .SetSpeedRange(0f, Speed)
              .SetScaleRange(0, Scale)
              .Emit(location[0] + control.Width / 2, location[1] + control.Height / 2, NumberOfParticles);

            Task.Run(async () =>
            {
                await Task.Delay(200);
                particleSystem?.StopEmitting();
            });
#endif

#if IOS
            var control = Control ?? Container;

            var effect = (Pairs.Effects.ParticleEffect)Element.Effects.FirstOrDefault(p => p is Pairs.Effects.ParticleEffect);

            if (effect is null)
            {
                return;
            }

            var lifeTime = effect.LifeTime;
            var numberOfItems = effect.NumberOfParticles;
            var scale = effect.Scale;
            var speed = effect.Speed * 1000;
            var image = effect.Image;

            var emitterLayer = new CAEmitterLayer
            {
                Position = new CoreGraphics.CGPoint(
                    control.Bounds.Location.X + control.Bounds.Width / 2,
                    control.Bounds.Location.Y + control.Bounds.Height / 2),
                Shape = CAEmitterLayer.ShapeCircle
            };

            var cell = new CAEmitterCell
            {
                Name = "pEmitter",
                BirthRate = numberOfItems,
                Scale = 0f,
                ScaleRange = scale,
                Velocity = speed,
                LifeTime = (float)lifeTime,
                EmissionRange = (nfloat)Math.PI * 2.0f,
                Contents = UIImage.FromBundle(image).CGImage
            };

            emitterLayer.Cells = new CAEmitterCell[] { cell };

            control.Layer.AddSublayer(emitterLayer);

            Task.Run(async () =>
            {
                await Task.Delay(100).ConfigureAwait(false);

                Device.BeginInvokeOnMainThread(() =>
                {
                    emitterLayer.SetValueForKeyPath(NSNumber.FromInt32(0), new NSString("emitterCells.pEmitter.birthRate"));
                });
            });
#endif
        }
    }
}
