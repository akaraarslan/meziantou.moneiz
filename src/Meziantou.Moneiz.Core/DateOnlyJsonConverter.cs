﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Meziantou.Moneiz.Core
{
    internal sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override bool HandleNull => false;

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out var dateTime))
                return DateOnly.FromDateTime(dateTime);

            var value = reader.GetString();
            if (DateOnly.TryParseExact(value, "yyyy-MM-dd", out var dateOnly))
                return dateOnly;

            throw new FormatException($"Cannot parse '{value}' to DateOnly");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
    }
}
