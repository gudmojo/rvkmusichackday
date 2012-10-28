<?php

/*
Deezer Ogg preview conversion class
Created by Aur�lien H�rault on 2012-07-06.
Contributor : Baptiste Bouillot, Fran�ois Lasserre, JP Carrascal
https://github.com/doky/deezer-ogg-preview
*/

class dzpreview{

	public static $dir_cache = 'stream/';

	public static function getPreview($track_id){

		$dir_cache = self::$dir_cache;

		if(!file_exists($dir_cache)){
			// test chmod before this line
			if(!@mkdir($dir_cache)){
		      		throw new Exception("Failed to create directory: $dir_cache.", 1);
			}
		}

		if(isset($track_id)){

			$track_id = (int) $track_id;
			//$data = self::getTrackData($track_id);
			//data = json_decode($data);
			$download_url = 'http://previews.7digital.com/clips/34/'.$track_id.'.clip.mp3';
			
			//if($data != false && !isset($data->error)){

				if(isset($download_url)){
				
					$file_info 	= parse_url($download_url);
					$file 		= str_replace('.mp3', '.ogg', basename($file_info['path']));
					
					if(!file_exists($dir_cache.$file[0])){
						mkdir($dir_cache.$file[0], 0777);
					}

					if(!file_exists($dir_cache.$file[0].'/'.$file[1])){
						mkdir($dir_cache.$file[0].'/'.$file[1]);
					}

					$file = $dir_cache.$file[0].'/'.$file[1].'/'.$file;

					$dir = dirname($file);

					self::clean_cache($dir);

					if(!file_exists($file)){
						
						exec('c:\ffmpeg\bin\ffmpeg -i "'.$download_url.'" -f ogg -strict experimental -acodec vorbis -ab 192k '.$file, $output, $return_var);

						return $file;

					}else{

						return $file;
					}
				}

			//}else{
			//	throw new Exception("No data", 1);
			//}

		}


	}

	public static function clean_cache($dir){

		if ($handle = opendir($dir)) {
			while (false !== ($entry = readdir($handle))) {
				if ($entry != "." && $entry != "..") {

					$timefile = filemtime($dir.'/'.$entry); 
					$time = time()-86400;
				
					if($timefile < $time){
						unlink($dir.'/'.$entry);
					}
				}
			}
		}
	}

	public static function getTrackData($track_id){

		if(isset($track_id)){

			$url  = "http://api.deezer.com/2.0/track/".$track_id;

			$ch = curl_init();

			curl_setopt($ch, CURLOPT_URL, $url);
			curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
			curl_setopt($ch, CURLOPT_HEADER, false);

			$data = curl_exec($ch);
			curl_close($ch);

			return $data;

		}else{
			return false;
		}
	}
	
	public static function getRedirect($url){
			$ch = curl_init();

			curl_setopt($ch, CURLOPT_URL, $url);
			curl_setopt($ch, CURLOPT_RETURNTRANSFER, false);
			curl_setopt($ch, CURLOPT_HEADER, false);

			$data = curl_exec($ch);
			curl_close($ch);

			$start = strpos($data,'<a href="');
			$end = strpos($data,'"',$start) + 8;
			$mail = substr($data,$start,$end-$start);
			return $mail;
	}

}

?>