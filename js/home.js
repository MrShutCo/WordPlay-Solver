function submit_analytics(letters, wasValid) {
    gtag('event', 'onsubmit', {
        'letters': letters,
        'isValid': wasValid,
    });
}

function runtime_analytics(letters, time, topresults) {
    gtag('event', 'runtime', {
        'letters': letters,
        'time': time,
        'topResults': topresults
    });
}