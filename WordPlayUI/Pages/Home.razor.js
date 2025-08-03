export function submit_analytics(letters, wasValid, modifiers, settings) {
    gtag('event', 'onsubmit', {
        'letters': letters,
        'isValid': wasValid,
        'modifiers': modifiers,
        'settings': JSON.stringify(settings)
    });
}

export function runtime_analytics(letters, time, topresults) {
    gtag('event', 'runtime', {
        'letters': letters,
        'time': time,
        'topResults': topresults
    });
}