﻿using System.Collections.Generic;
using FluentAssertions;
using MLS.Agent.JsonContracts;
using Newtonsoft.Json;
using WorkspaceServer.Models.Execution;
using Xunit;

namespace WorkspaceServer.Tests
{
    public class JsonContractsTests
    {
        public JsonContractsTests()
        {
            var settings = JsonConvert.DefaultSettings?.Invoke() ?? new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter> { new WorkspaceRequestConverter() }
            };

            JsonConvert.DefaultSettings = () => settings;
        }

        [Fact]
        public void can_augment_workspace_to_workpaceRequest()
        {
            var json = JsonConvert.SerializeObject(new
            {
                workspaceType = "console",
                buffers = new[] { new { id = "testId", content = "no code", position = 0 } }
            });

            var request = JsonConvert.DeserializeObject<WorkspaceRequest>(json);
            request.Should().NotBeNull();
        }
    }
}
