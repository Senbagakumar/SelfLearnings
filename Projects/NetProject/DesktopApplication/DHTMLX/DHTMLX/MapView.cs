using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;

namespace DHTMLX.Scheduler.Controls
{
    public class MapView : AgendaView
    {
        public MapView()
        {
            this._hiddenProperties.Add("label");
            this._hiddenProperties.Add("tabPosition");
            this._hiddenProperties.Add("view_type");
            this._hiddenProperties.Add("name");
            this.ViewType = "map";
            this.Set("name", (object)"map");
            this.Label = "Map";
            this.TabPosition = 280;
        }

        public override Dictionary<string, FileType> GetJS()
        {
            return new Dictionary<string, FileType>()
      {
        {
          "//maps.google.com/maps/api/js?sensor=false",
          FileType.Remote
        },
        {
          "ext/dhtmlxscheduler_map_view.js",
          FileType.Local
        }
      };
        }

        private double iLat { get; set; }

        private double iLng { get; set; }

        private double eLat { get; set; }

        private double eLng { get; set; }

        public void SetInitialPosition(double lat, double lng)
        {
            this.iLat = lat;
            this.iLng = lng;
        }

        public void SetErrorPosition(double lat, double lng)
        {
            this.eLat = lat;
            this.eLng = lng;
        }

        public string SectionLocation
        {
            get
            {
                return this.Get("{0}.locale.labels.section_location");
            }
            set
            {
                this.Set("{0}.locale.labels.section_location", (object)value);
            }
        }

        public string MarkerGeoSuccess
        {
            get
            {
                return this.Get("{0}.locale.labels.marker_geo_success");
            }
            set
            {
                this.Set("{0}.locale.labels.marker_geo_success", (object)value);
            }
        }

        public string MarkerGeoFail
        {
            get
            {
                return this.Get("{0}.locale.labels.marker_geo_fail");
            }
            set
            {
                this.Set("{0}.locale.labels.marker_geo_fail", (object)value);
            }
        }

        public override void Render(StringBuilder builder, string parent)
        {
            base.Render(builder, parent);
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            foreach (KeyValuePair<string, object> visibleProperty in this.GetVisibleProperties())
            {
                builder.Append("\n");
                builder.Append(string.Format(visibleProperty.Key, (object)parent));
                builder.Append("= ");
                builder.Append(scriptSerializer.Serialize(visibleProperty.Value));
                builder.Append(";");
            }
            if (this.iLat != 0.0 && this.iLng != 0.0)
                builder.Append(string.Format("\n{0}.config.map_initial_position = new google.maps.LatLng({1}, {2});", (object)parent, (object)this.iLat, (object)this.iLng));
            if (this.iLat == 0.0 || this.iLng == 0.0)
                return;
            builder.Append(string.Format("\n{0}.config.map_error_position = new google.maps.LatLng({1}, {2});", (object)parent, (object)this.eLat, (object)this.eLng));
        }
    }
}

