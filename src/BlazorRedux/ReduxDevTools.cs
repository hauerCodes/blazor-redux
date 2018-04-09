﻿using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;

namespace BlazorRedux
{
    public class ReduxDevTools : BlazorComponent
    {
        // ReSharper disable once RedundantAssignment
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;

            builder.OpenElement(seq++, "script");
            builder.AddContent(seq++,
@"(function () {
Blazor.registerFunction('log', (action, state) => {
        console.log(action);
        console.log(state);
        window.devTools.send(action, state);
        return true;
    });

var config = { name: 'Blazor Redux' }; 
var extension = window.__REDUX_DEVTOOLS_EXTENSION__;

if (!extension) {
    console.log('Redux DevTools not installed.');
    return;
}

var devTools = extension.connect(config);

if (!devTools) {
    console.log('Unable to connect to Redux DevTools.');
    return;
}

devTools.subscribe((message) => {
    if (message.type === 'DISPATCH' && message.state) {
        console.log('DevTools requested to change the state to ', message.state);
    }
});

window.devTools = devTools;
console.log('Connected with Redux DevTools');

devTools.init({ value: 'initial state' });
devTools.send('change state', { value: 'state changed' });
devTools.error('Foo Bar error');
devTools.send('change state', { value: 'state changed 2' });

const devToolsReady = Blazor.platform.findMethod('BlazorRedux', 'BlazorRedux', 'DevToolsInterop', 'DevToolsReady');
Blazor.platform.callMethod(devToolsReady, null, []);
}());");

            builder.CloseElement();
        }
    }
}
