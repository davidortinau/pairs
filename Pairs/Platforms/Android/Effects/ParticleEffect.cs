using System.Linq;
using AndroidX.Core.Content;
using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Platform;
using Com.Plattysoft.Leonids;

namespace Pairs.Droid.Effects
{
    public class ParticleEffect : PlatformEffect
    {
        private Pairs.Effects.ParticleEffect particleEffect;

        protected override void OnAttached()
        {
            var effect = Element.Effects.OfType<Pairs.Effects.ParticleEffect>().FirstOrDefault();

            if (effect != null)
            {
                particleEffect = effect;
                effect.Emit += OnEffectEmit;
            }
        }

        protected override void OnDetached()
        {
            if (particleEffect != null)
            {
                particleEffect.Emit -= OnEffectEmit;
            }
        }

        private void OnEffectEmit(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var effect = (Pairs.Effects.ParticleEffect)Element.Effects.FirstOrDefault(p => p is Pairs.Effects.ParticleEffect);

            var lifeTime = (long)(effect?.LifeTime * 1000 ?? (long)1500);
            var numberOfItems = effect?.NumberOfParticles ?? 4000;
            var scale = effect?.Scale ?? 1.0f;
            var speed = effect?.Speed ?? 0.1f;
            var image = effect?.Image ?? "ic_launcher";

            var location = new int[2];
            control.GetLocationOnScreen(location);

            var drawableImage = ContextCompat.GetDrawable(Microsoft.Maui.Essentials.Platform.CurrentActivity, Microsoft.Maui.Essentials.Platform.CurrentActivity.Resources.GetIdentifier(image, "drawable", Microsoft.Maui.Essentials.Platform.CurrentActivity.PackageName));
            var particleSystem = new ParticleSystem(Microsoft.Maui.Essentials.Platform.CurrentActivity, numberOfItems, drawableImage, lifeTime);
            particleSystem
              .SetSpeedRange(0f, speed)
              .SetScaleRange(0, scale)
              .Emit(location[0] + control.Width / 2, location[1] + control.Height / 2, numberOfItems);

            Task.Run(async () =>
            {
                await Task.Delay(200);
                particleSystem?.StopEmitting();
            });
        }


    }
}