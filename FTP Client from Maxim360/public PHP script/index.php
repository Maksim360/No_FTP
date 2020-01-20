<?php
	//require 'db.php';
	
	$permitted_chars = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	date_default_timezone_set('Europe/Moscow');
	$data = $_POST;
    $Time = date("Y-m-d H:i:s");
	$ip = @$_SERVER['REMOTE_ADDR'];

		switch ($data['command']) {
			case ChekLink:
				echo '46436';
		break;
	}
?>