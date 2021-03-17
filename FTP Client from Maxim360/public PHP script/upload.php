﻿<?php
//Дата (Д М Г _ Ч М С)
$date_today = date("m.d.y.H.i.s");
$ip = "1";
		switch ($_POST['command']) {
			case ChekLink:
				echo '46436';
		break;
	}
$AllowedIP = array("$AllowedIP1", "$AllowedIP2");

if (in_array($ip, $AllowedIP)) {
       //echo "<br>Ваш IP подходит для загрузки!!!</br>";
		
		echo '<table border="1" align="center">';
		echo "<tr>";
		echo "<td>id</td>";
		echo "<td>название</td>";
		echo "<td>ссылка</td>";
		echo "<td>дата</td>";
		echo "<td>размер</td>";
		echo "<td>тип</td>";
        echo "</tr>";
	
	
	$path = __DIR__ . '/uploads/';
    $filelist = array();
	$dir = "uploads";
	$i;
    if($handle = opendir($dir)){

        while(false !== ($file = readdir($handle))) {
        if($file != "." && $file != ".."){
		$i++;
	    echo "<tr>";
	    echo "<td>$i</td>";
	    echo "<td>$file</td>";
		echo "<td><a href=/uploads/{$file}>скачать файл</a></td>";
		$FileData = date("F d Y H:i:s.", filemtime($file));
		$filesize = filesize($file);
		$filetype = filetype($file);
		echo "<td>$FileData</td>";//дата обращения к файлу
        echo "<td>$filesize</td>";//размер
        echo "<td>$filetype</td>"; //тип
		echo "</tr>";
        }
    }
}
}
else
{
 	echo "Ваш IP неподходит для загрузки!";
	exit();
}
// Название <input type="file">
$input_name = 'file';
 
// Разрешенные расширения файлов.
$allow = array();
 
// Запрещенные расширения файлов.
$deny = array(
	'phtml', 'php', 'php3', 'php4', 'php5', 'php6', 'php7', 'phps', 'cgi', 'pl', 'asp', 
	'aspx', 'shtml', 'shtm', 'htaccess', 'htpasswd', 'ini', 'log', 'sh', 'js', 'html', 
	'htm', 'css', 'sql', 'spl', 'scgi', 'fcgi','scr','exe'
);
 
// Директория куда будут загружаться файлы.
$path = __DIR__ . '/uploads/';
 
if (isset($_FILES[$input_name])) {
	// Проверим директорию для загрузки.
	if (!is_dir($path)) {
		mkdir($path, 0777, true);
	}
 
	// Преобразуем массив $_FILES в удобный вид для перебора в foreach.
	$files = array();
	$diff = count($_FILES[$input_name]) - count($_FILES[$input_name], COUNT_RECURSIVE);
	if ($diff == 0) {
		$files = array($_FILES[$input_name]);
	} else {
		foreach($_FILES[$input_name] as $k => $l) {
			foreach($l as $i => $v) {
				$files[$i][$k] = $v;
			}
		}		
	}	
	
	foreach ($files as $file) {
		$error = $success = '';
 
		// Проверим на ошибки загрузки.
		if (!empty($file['error']) || empty($file['tmp_name'])) {
			switch (@$file['error']) {
				case 1:
				case 2: $error = 'Превышен размер загружаемого файла.'; break;
				case 3: $error = 'Файл был получен только частично.'; break;
				case 4: $error = 'Файл не был загружен.'; break;
				case 6: $error = 'Файл не загружен - отсутствует временная директория.'; break;
				case 7: $error = 'Не удалось записать файл на диск.'; break;
				case 8: $error = 'PHP-расширение остановило загрузку файла.'; break;
				case 9: $error = 'Файл не был загружен - директория не существует.'; break;
				case 10: $error = 'Превышен максимально допустимый размер файла.'; break;
				case 11: $error = 'Данный тип файла запрещен.'; break;
				case 12: $error = 'Ошибка при копировании файла.'; break;
				default: $error = 'Файл не был загружен - неизвестная ошибка.'; break;
			}
		} elseif ($file['tmp_name'] == 'none' || !is_uploaded_file($file['tmp_name'])) {
			$error = 'Не удалось загрузить файл.';
		} else {
			// Оставляем в имени файла только буквы, цифры и некоторые символы.
			$pattern = "[^a-zа-яё0-9,~!@#%^-_\$\?\(\)\{\}\[\]\.]";
			$name = mb_eregi_replace($pattern, '-', $file['name']);
			$name = mb_ereg_replace('[-]+', '-', $name);
			
			// Т.к. есть проблема с кириллицей в названиях файлов (файлы становятся недоступны).
			// Сделаем их транслит:
			$converter = array(
				'а' => 'a',   'б' => 'b',   'в' => 'v',    'г' => 'g',   'д' => 'd',   'е' => 'e',
				'ё' => 'e',   'ж' => 'zh',  'з' => 'z',    'и' => 'i',   'й' => 'y',   'к' => 'k',
				'л' => 'l',   'м' => 'm',   'н' => 'n',    'о' => 'o',   'п' => 'p',   'р' => 'r',
				'с' => 's',   'т' => 't',   'у' => 'u',    'ф' => 'f',   'х' => 'h',   'ц' => 'c',
				'ч' => 'ch',  'ш' => 'sh',  'щ' => 'sch',  'ь' => '',    'ы' => 'y',   'ъ' => '',
				'э' => 'e',   'ю' => 'yu',  'я' => 'ya', 
			
				'А' => 'A',   'Б' => 'B',   'В' => 'V',    'Г' => 'G',   'Д' => 'D',   'Е' => 'E',
				'Ё' => 'E',   'Ж' => 'Zh',  'З' => 'Z',    'И' => 'I',   'Й' => 'Y',   'К' => 'K',
				'Л' => 'L',   'М' => 'M',   'Н' => 'N',    'О' => 'O',   'П' => 'P',   'Р' => 'R',
				'С' => 'S',   'Т' => 'T',   'У' => 'U',    'Ф' => 'F',   'Х' => 'H',   'Ц' => 'C',
				'Ч' => 'Ch',  'Ш' => 'Sh',  'Щ' => 'Sch',  'Ь' => '',    'Ы' => 'Y',   'Ъ' => '',
				'Э' => 'E',   'Ю' => 'Yu',  'Я' => 'Ya',
			);
 
			$name = strtr($name, $converter);
			$parts = pathinfo($name);
 
			if (empty($name) || empty($parts['extension'])) {
				$error = 'Недопустимое тип файла';
			} elseif (!empty($allow) && !in_array(strtolower($parts['extension']), $allow)) {
				$error = 'Недопустимый тип файла';
			} elseif (!empty($deny) && in_array(strtolower($parts['extension']), $deny)) {
				$error = 'Недопустимый тип файла';
			} else {
				// Чтобы не затереть файл с таким же названием, добавим префикс.
				$i = 0;
				$prefix = "_date_$date_today";
				while (is_file($path . $parts['filename'] . $prefix . '.' . $parts['extension'])) {
		  			$prefix = '(' . ++$i . ')';
				}
				$name = $parts['filename'] . $prefix . '.' . $parts['extension'];
 
				// Перемещаем файл в директорию.
				if (move_uploaded_file($file['tmp_name'], $path . $name)) {
					// Далее можно сохранить название файла в БД и т.п.
					$success = 'Файл «' . $name . '» успешно загружен.';
				} else {
					$error = 'Не удалось загрузить файл.';
				}
			}
		}
		
		// Выводим сообщение о результате загрузки.
		if (!empty($success)) {
			echo '<p>' . $success . '</p>';		
		} else {
			echo '<p>' . $error . '</p>';
		}
	}
}?>


<form action="" method="post" enctype="multipart/form-data">
	<input type="file" name="file[]" multiple>
	<input type="submit" value="Отправить">
</form>

