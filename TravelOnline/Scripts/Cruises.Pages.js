if (screen.width >= 1280) {
    $('#photos').galleryView({
        panel_width: 550,
        panel_height: 350,
        overlay_height: 35,
        frame_width: 60,
        frame_height: 40,
        overlay_color: '#4D4D4D',
        background_color: '#4D4D4D',
        border: '1px solid #256503',
        overlay_opacity: 0.5
    });
}
else {
    $('#photos').galleryView({
        panel_width: 440,
        panel_height: 300,
        overlay_height: 35,
        frame_width: 60,
        frame_height: 40,
        overlay_color: '#4D4D4D',
        background_color: '#4D4D4D',
        border: '1px solid #256503',
        overlay_opacity: 0.5
    });
}


