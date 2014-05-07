using System;
using System.Collections.Generic;
using EventStore.Projections.Core.Utils;

namespace EventStore.Projections.Core.Services.Processing
{
    public class EmittedDataEvent : EmittedEvent
    {
        private readonly byte[] _data;
        private readonly ExtraMetaData _metadata;
        private readonly bool _isJson;

        public EmittedDataEvent(
            string streamId,
            Guid eventId,
            string eventType,
            bool isJson,
            byte[] data,
            ExtraMetaData metadata,
            CheckpointTag causedByTag,
            CheckpointTag expectedTag,
            Action<int> onCommitted = null)
            : base(streamId, eventId, eventType, causedByTag, expectedTag, onCommitted)
        {
            _isJson = isJson;
            _data = data;
            _metadata = metadata;
        }

        public EmittedDataEvent(
            string streamId,
            Guid eventId,
            string eventType,
            bool isJson,
            string data,
            ExtraMetaData metadata,
            CheckpointTag causedByTag,
            CheckpointTag expectedTag,
            Action<int> onCommitted = null)
            : this(streamId, eventId, eventType, isJson, data.ToUtf8(), metadata, causedByTag, expectedTag, onCommitted)
        {
        }

        public override byte[] Data
        {
            get { return _data; }
        }

        public ExtraMetaData Metadata
        {
            get { return _metadata; }
        }

        public override bool IsJson
        {
            get { return _isJson; }
        }

        public override bool IsReady()
        {
            return true;
        }

        public override IEnumerable<KeyValuePair<string, string>> ExtraMetaData()
        {
            return _metadata == null ? null : _metadata.Metadata;
        }
    }
}
