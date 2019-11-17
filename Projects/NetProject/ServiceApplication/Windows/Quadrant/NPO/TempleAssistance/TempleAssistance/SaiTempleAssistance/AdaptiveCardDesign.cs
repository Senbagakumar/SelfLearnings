using AdaptiveCards;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaiTempleAssistance
{
    public class AdaptiveCardDesign : ICard
    {
        private string Name { get; set; }
        private string PriceOrTime {get;set;}
        public int BoldStyleIndex { get ; set; }

        public AdaptiveCardDesign(string name,string priceortime)
        {
            Name = name;
            PriceOrTime = priceortime;
        }

        public Attachment LoopCardAdaptiveCard(List<TempleTiming> output)
        {
            var columns = new List<Column>();
            var fnNames = new List<CardElement>();
            var fnTimings = new List<CardElement>();

            //Add header
            output.Insert(0, new TempleTiming() { FunctionName = Name, FunctionTimings = PriceOrTime });

            foreach (var oput in output)
            {
                fnNames.Add(new TextBlock() { Text = oput.FunctionName, Separation = SeparationStyle.Strong, Size = TextSize.Small });
                fnTimings.Add(new TextBlock() { Text = oput.FunctionTimings, Separation = SeparationStyle.Strong, Size = TextSize.Small });
            }

           (fnNames[0] as TextBlock).Weight = TextWeight.Bolder;
            (fnTimings[0] as TextBlock).Weight = TextWeight.Bolder;

            (fnNames[BoldStyleIndex] as TextBlock).Weight = TextWeight.Bolder;
            (fnTimings[BoldStyleIndex] as TextBlock).Weight = TextWeight.Bolder;

            for (int i = 0; i < 2; i++)
            {
                var column = new Column() { Style = ContainerStyle.Normal };
                if (i == 0) { column.Items = fnNames; }
                if (i == 1) { column.Items = fnTimings; }
                columns.Add(column);
            }

            var container = new Container() { Items = new List<CardElement>() { new ColumnSet() { Columns = columns } } };
            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = new AdaptiveCard() { Body = new List<CardElement>() { container } }
            };

        }

    }
}
