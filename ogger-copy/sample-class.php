<?php

/* Sample */

require 'class.dzpreview.php';

$track_id = $_GET["trackid"];
$baseurl = 'http://localhost/ogger/';

$path = dzpreview::getPreview($track_id);

echo $baseurl.$path;

?>