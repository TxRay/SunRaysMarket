function getElementWidthById(id) {
    return document.getElementById(id).offsetWidth;
}


function watchElementSize(elementId, dotNetObj) {
    const element = document.getElementById(elementId);

    const watch = async () => {

        await dotNetObj.invokeMethodAsync("SetHeight", element.offsetHeight);
        await dotNetObj.invokeMethodAsync("SetWidth", element.offsetWidth)
    }

    const observer = new ResizeObserver(watch);
    observer.observe(element);
}

