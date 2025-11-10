# Country/State/City Cascading Dropdowns Partial View

## Overview
This partial view provides a reusable component for implementing cascading dropdowns for Country, State, and City selections across multiple views in the application.

## Usage

### Basic Usage
To use the partial view in your Razor view:

```html
@{
    ViewData["CountryProperty"] = "CountryId";
    ViewData["StateProperty"] = "StateId";
    ViewData["CityProperty"] = "CityId";
    ViewData["CountryLabel"] = "Country";
    ViewData["StateLabel"] = "State";
    ViewData["CityLabel"] = "City";
    ViewData["ControllerName"] = "YourControllerName";
}
<partial name="_CountryStateCityDropdowns" model="@Model" view-data="ViewData" />
```

### Parameters
- `CountryProperty`: The name of the property for the country dropdown (default: "CountryId")
- `StateProperty`: The name of the property for the state dropdown (default: "StateId")
- `CityProperty`: The name of the property for the city dropdown (default: "CityId")
- `CountryLabel`: The label text for the country dropdown (default: "Country")
- `StateLabel`: The label text for the state dropdown (default: "State")
- `CityLabel`: The label text for the city dropdown (default: "City")
- `ControllerName`: The name of the controller that handles the GetStates and GetCities actions (default: "")

### Model Requirements
Your model should have:
- Properties for CountryId, StateId, and CityId (or custom names as specified)
- Collections of SelectListItem for Countries, States, and Cities

### Controller Requirements
Your controller should have the following actions:
```csharp
[HttpGet]
[Route("GetStates")]
public IActionResult GetStates(Guid countryId)
{
    var list = _lookup.GetStates(countryId).Select(s => new { id = s.Id, name = s.Name });
    return Ok(list);
}

[HttpGet]
[Route("GetCities")]
public IActionResult GetCities(Guid stateId)
{
    var list = _lookup.GetCities(stateId).Select(c => new { id = c.Id, name = c.Name });
    return Ok(list);
}
```

### JavaScript
The cascading functionality is handled by the `cascading-dropdowns.js` file which is included in the `_Layout.cshtml` file. You don't need to include any additional JavaScript in your views.

## Example
See `Views/Vendor/Create.cshtml` and `Views/Vendor/Edit.cshtml` for examples of how to use this partial view.

## Customization
You can customize the labels and property names by setting the appropriate ViewData values as shown in the usage example above.