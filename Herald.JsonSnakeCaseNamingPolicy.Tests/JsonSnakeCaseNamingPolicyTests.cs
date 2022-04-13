using Microsoft.Extensions.DependencyInjection;

using System.Text.Json;

using Xunit;

namespace Herald.JsonSnakeCaseNamingPolicy.Tests
{
    public class UserStub
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ExternalUserId { get; set; }
    }

    public class JsonSnakeCaseNamingPolicyTests
    {
        [Fact]
        public void ShouldAddSnakeCaseNamingPolicyAsPropertyNamingPolicy()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();

            //Act
            serviceCollection.AddSnakeCaseNamingPolicy();

            //Assert
            var defaultJsonOptions = ((JsonSerializerOptions)typeof(JsonSerializerOptions)
                .GetField("s_defaultOptions", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .GetValue(null));

            Assert.IsType<JsonSnakeCaseNamingPolicy>(defaultJsonOptions.PropertyNamingPolicy);
        }

        [Fact]
        public void ShouldSerializeWithSnakeCase()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var userStub = new UserStub() { UserId = 1, Name = "myname", ExternalUserId = "06a73fc9-af1a-4471-a1f0-1a3955e9434d" };
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy() };

            //Act
            var json = JsonSerializer.Serialize(userStub, jsonSerializerOptions);

            //Assert
            Assert.Contains("user_id", json);
            Assert.Contains("name", json);
            Assert.Contains("external_user_id", json);
        }

        [Fact]
        public void ShouldDeserializeWithSnakeCase()
        {
            //Arrange
            var serviceCollection = new ServiceCollection();
            var userId = 1;
            var name = "myname";
            var externalUserId = "06a73fc9-af1a-4471-a1f0-1a3955e9434d";
            var json = $"{{\"user_id\":{userId},\"name\":\"{name}\",\"external_user_id\":\"{externalUserId}\"}}";
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy() };

            //Act
            var obj = JsonSerializer.Deserialize<UserStub>(json, jsonSerializerOptions);

            //Assert
            Assert.Equal(name, obj.Name);
            Assert.Equal(userId, obj.UserId);
            Assert.Equal(externalUserId, obj.ExternalUserId);
        }
    }
}