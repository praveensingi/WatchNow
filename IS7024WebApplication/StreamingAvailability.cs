﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using IS7024WebApplication;
//
//    var streamingAvailability = StreamingAvailability.FromJson(jsonString);

namespace IS7024WebApplication
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class StreamingAvailability
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("age")]
        public long Age { get; set; }

        [JsonProperty("backdropPath")]
        public string BackdropPath { get; set; }

        [JsonProperty("backdropURLs")]
        public BackdropUrLs BackdropUrLs { get; set; }

        [JsonProperty("cast")]
        public List<string> Cast { get; set; }

        [JsonProperty("countries")]
        public List<string> Countries { get; set; }

        [JsonProperty("genres")]
        public List<long> Genres { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbId { get; set; }

        [JsonProperty("imdbRating")]
        public long ImdbRating { get; set; }

        [JsonProperty("imdbVoteCount")]
        public long ImdbVoteCount { get; set; }

        [JsonProperty("originalTitle")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("posterPath")]
        public string PosterPath { get; set; }

        [JsonProperty("posterURLs")]
        public PosterUrLs PosterUrLs { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("significants")]
        public List<string> Significants { get; set; }

        [JsonProperty("streamingInfo")]
        public StreamingInfo StreamingInfo { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("tmdbID")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TmdbId { get; set; }

        [JsonProperty("video")]
        public string Video { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }
    }

    public partial class BackdropUrLs
    {
        [JsonProperty("300")]
        public Uri The300 { get; set; }

        [JsonProperty("780")]
        public Uri The780 { get; set; }

        [JsonProperty("1280")]
        public Uri The1280 { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }
    }

    public partial class PosterUrLs
    {
        [JsonProperty("92")]
        public Uri The92 { get; set; }

        [JsonProperty("154")]
        public Uri The154 { get; set; }

        [JsonProperty("185")]
        public Uri The185 { get; set; }

        [JsonProperty("342")]
        public Uri The342 { get; set; }

        [JsonProperty("500")]
        public Uri The500 { get; set; }

        [JsonProperty("780")]
        public Uri The780 { get; set; }

        [JsonProperty("original")]
        public Uri Original { get; set; }
    }

    public partial class StreamingInfo
    {
        [JsonProperty("netflix")]
        public Netflix Netflix { get; set; }
    }

    public partial class Netflix
    {
        [JsonProperty("us")]
        public Us Us { get; set; }
    }

    public partial class Us
    {
        [JsonProperty("added")]
        public long Added { get; set; }

        [JsonProperty("leaving")]
        public long Leaving { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }
    }

    public partial class StreamingAvailability
    {
        public static StreamingAvailability FromJson(string json) => JsonConvert.DeserializeObject<StreamingAvailability>(json, IS7024WebApplication.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this StreamingAvailability self) => JsonConvert.SerializeObject(self, IS7024WebApplication.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}