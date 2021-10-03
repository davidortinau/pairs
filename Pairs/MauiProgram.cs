using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Hosting;
using Pairs.Effects;

namespace Pairs
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				})
				.ConfigureEffects(effects =>
            {
				#if ANDROID
				effects.Add(typeof(ParticleEffect), typeof(Pairs.Droid.Effects.ParticleEffect));
				#endif
            });

			return builder.Build();
		}
	}
}