using DHTMLX.Scheduler.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DHTMLX.Scheduler
{
    public class SchedulerLocalization : SchedulerControlsBase
    {
        protected Dictionary<string, FileType> _files = new Dictionary<string, FileType>();
        protected Dictionary<SchedulerLocalization.Localizations, string[]> translate = new Dictionary<SchedulerLocalization.Localizations, string[]>()
    {
      {
        SchedulerLocalization.Localizations.Arabic,
        new string[1]{ "locale_ar.js" }
      },
      {
        SchedulerLocalization.Localizations.English,
        new string[0]
      },
      {
        SchedulerLocalization.Localizations.Catalan,
        new string[1]{ "locale_ca.js" }
      },
      {
        SchedulerLocalization.Localizations.Chinese,
        new string[2]{ "locale_cn.js", "locale_recurring_cn.js" }
      },
      {
        SchedulerLocalization.Localizations.Czech,
        new string[2]{ "locale_cs.js", "locale_recurring_cs.js" }
      },
      {
        SchedulerLocalization.Localizations.Danish,
        new string[2]{ "locale_da.js", "locale_recurring_da.js" }
      },
      {
        SchedulerLocalization.Localizations.Dutch,
        new string[1]{ "locale_nl.js" }
      },
      {
        SchedulerLocalization.Localizations.Finnish,
        new string[2]{ "locale_fi.js", "locale_recurring_fi.js" }
      },
      {
        SchedulerLocalization.Localizations.French,
        new string[2]{ "locale_fr.js", "locale_recurring_fr.js" }
      },
      {
        SchedulerLocalization.Localizations.German,
        new string[2]{ "locale_de.js", "locale_recurring_de.js" }
      },
      {
        SchedulerLocalization.Localizations.Greek,
        new string[2]{ "locale_el.js", "locale_recurring_el.js" }
      },
      {
        SchedulerLocalization.Localizations.Hebrew,
        new string[1]{ "locale_he.js" }
      },
      {
        SchedulerLocalization.Localizations.Hungarian,
        new string[1]{ "locale_hu.js" }
      },
      {
        SchedulerLocalization.Localizations.Indonesia,
        new string[1]{ "locale_id.js" }
      },
      {
        SchedulerLocalization.Localizations.Italian,
        new string[2]{ "locale_it.js", "locale_recurring_it.js" }
      },
      {
        SchedulerLocalization.Localizations.Japanese,
        new string[1]{ "locale_jp.js" }
      },
      {
        SchedulerLocalization.Localizations.Norwegian,
        new string[2]{ "locale_no.js", "locale_recurring_nl.js" }
      },
      {
        SchedulerLocalization.Localizations.Polish,
        new string[2]{ "locale_pl.js", "locale_recurring_pl.js" }
      },
      {
        SchedulerLocalization.Localizations.Portuguese,
        new string[2]{ "locale_pt.js", "locale_recurring_pt.js" }
      },
      {
        SchedulerLocalization.Localizations.Russian,
        new string[2]{ "locale_ru.js", "locale_recurring_ru.js" }
      },
      {
        SchedulerLocalization.Localizations.Slovenian,
        new string[1]{ "locale_si.js" }
      },
      {
        SchedulerLocalization.Localizations.Spanish,
        new string[2]{ "locale_es.js", "locale_recurring_es.js" }
      },
      {
        SchedulerLocalization.Localizations.Swedish,
        new string[2]{ "locale_sv.js", "locale_recurring_sv.js" }
      },
      {
        SchedulerLocalization.Localizations.Turkish,
        new string[1]{ "locale_tr.js" }
      },
      {
        SchedulerLocalization.Localizations.Ukrainian,
        new string[2]{ "locale_ua.js", "locale_recurring_ua.js" }
      }
    };

        public string Directory { get; set; }

        internal override Dictionary<string, FileType> GetJS()
        {
            return this._files;
        }

        public override void Clear()
        {
            this._files.Clear();
        }

        public override bool IsSet()
        {
            return this._files.Count != 0;
        }

        public void Set(SchedulerLocalization.Localizations locale)
        {
            this.Set(locale, true);
        }

        public void Set(SchedulerLocalization.Localizations locale, bool recurring)
        {
            this.Clear();
            string[] strArray = this.translate[locale];
            if (strArray.Length == 0)
                return;
            this._files.Add((!string.IsNullOrEmpty(this.Directory) ? this.Directory + "/" : "") + strArray[0], FileType.Local);
            if (!recurring || strArray.Length <= 1)
                return;
            this._files.Add((!string.IsNullOrEmpty(this.Directory) ? this.Directory + "/" : "") + strArray[1], FileType.Local);
        }

        public void Set(string locale)
        {
            this.Set(locale, true);
        }

        public void Set(string locale, bool recurring)
        {
            this.Clear();
            List<string> stringList = new List<string>();
            this._files.Add(string.Format("{0}locale_{1}.js", !string.IsNullOrEmpty(this.Directory) ? (object)(this.Directory + "/") : (object)"", (object)locale), FileType.Local);
            if (!recurring)
                return;
            this._files.Add(string.Format("{0}locale_recurring_{1}.js", !string.IsNullOrEmpty(this.Directory) ? (object)(this.Directory + "/") : (object)"", (object)locale), FileType.Local);
        }

        public override void Render(StringBuilder builder, string parent)
        {
        }

        public enum Localizations
        {
            Arabic,
            English,
            Catalan,
            Chinese,
            Czech,
            Danish,
            Dutch,
            Finnish,
            French,
            German,
            Greek,
            Hebrew,
            Hungarian,
            Indonesia,
            Italian,
            Japanese,
            Norwegian,
            Polish,
            Portuguese,
            Russian,
            Slovenian,
            Spanish,
            Swedish,
            Turkish,
            Ukrainian,
        }
    }
}

