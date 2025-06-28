window.toggleDarkMode = function (enable) {
    document.body.classList.toggle('dark', enable);
    localStorage.setItem('darkMode', enable ? '1' : '0');
}

window.initDarkMode = function () {
    const isDark = localStorage.getItem('darkMode') === '1';
    document.body.classList.toggle('dark', isDark);
    return isDark;
}

window.scrollTo(xCoord, yCoord) = function () {
    window.scrollTo({
        top: yCoord,
        left: xCoord,
        behavior: 'smooth'
    });
}