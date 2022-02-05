using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure.Attributes;
using System.Dynamic;
using System.Reflection;

namespace OrderManager.ApplicationCore.Features.Triggers;

public class TriggerController {

    private readonly ISender _sender;

    public TriggerController(ISender sender) {
        _sender = sender;
    }

    public Task<TriggerConfiguration> CreateTrigger() {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TriggerConfiguration>> GetAllTrigger() {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TriggerConfiguration>> GetTriggersByType(TriggerType TriggerType) {
        throw new NotImplementedException();
    }
    
    public Task FireTrigger(Trigger trigger) {
        return _sender.Send(new FireTrigger.Command(trigger));
    }

    /// <summary>
    /// Expands the properties 
    /// </summary>
    /// <param name="source">A model to expand</param>
    /// <returns>A model with all properties expanded</returns>
    public static dynamic GenerateExpandedModel(object source) {

        ExpandoObject model = new();

        if (model is null) throw new InvalidDataException("Could not create model");
        var collection = model as ICollection<KeyValuePair<string, object>>;

        var properties = source.GetType().GetProperties();

        foreach (PropertyInfo property in properties) {
            var value = property.GetValue(source);
            if (value is null) continue;

            bool hasAttribute = property.GetCustomAttribute<ExpandableProperty>() != null;
            if (hasAttribute) {

                var expandableProp = (ICollection<KeyValuePair<string, object>>)value;

                ExpandoObject subModel = new();
                var subCollection = model as ICollection<KeyValuePair<string, object>>;

                foreach (var subProp in expandableProp) {
                    if (System.Attribute.IsDefined(property.GetType(), typeof(ExpandableModel)))
                        collection.Add(new(
                            key: subProp.Key,
                            value: GenerateExpandedModel(subProp.Value)));
                    else collection.Add(new(subProp.Key, subProp.Value));
                }

            } else if (System.Attribute.IsDefined(property.GetType(), typeof(ExpandableModel))) {

                collection.Add(new(
                    key: property.Name,
                    value: GenerateExpandedModel(value)));

            } else collection.Add(new(property.Name, value));
        }

        return model;

    }

}