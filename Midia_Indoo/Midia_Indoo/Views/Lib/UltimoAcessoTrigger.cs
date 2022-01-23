using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Midia_Indoo.Views.Lib
{
    public class UltimoAcessoTrigger : TriggerAction<Entry>
    {
        protected async override void Invoke(Entry sender)
        {
            if (sender.Text != null)
            {
                await Task.Delay(1000);
                var result = await new PlayeReqService().GetAllPlayeReqsId(int.Parse(sender.Text));
                if (result == null)
                {
                    sender.BackgroundColor = Color.White;

                }
                else
                {
                    if (result.IsSuccess)
                    {
                        var min = result.Data.QtdMinutos;
                        if (min <= 190)
                            sender.BackgroundColor = Color.Green;
                        if (min > 190 && min <= 235)
                            sender.BackgroundColor = Color.Yellow;
                        if (min >= 240)
                            sender.BackgroundColor = Color.Red;
                        if (min >= 411738)
                            sender.BackgroundColor = Color.White;
                    }
                }
            }

        }
    }
}
