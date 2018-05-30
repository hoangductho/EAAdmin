/*
 * CKFinder
 * ========
 * http://ckfinder.com
 * Copyright (C) 2007-2011, CKSource - Frederico Knabben. All rights reserved.
 *
 * The software, this file and its contents are subject to the CKFinder
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of CKFinder.
 *
 */

/**
 * @fileOverview Defines the {@link CKFinder.lang} object, for the English
 *		language. This is the base file for all translations.
 */

/**
 * Constains the dictionary of language entries.
 * @namespace
 */
CKFinder.lang['en'] =
{
	appTitle : 'CKFinder',

	// Common messages and labels.
	common :
	{
		// Put the voice-only part of the label in the span.
		unavailable		: '%1<span class="cke_accessibility">, unavailable</span>',
		confirmCancel	: 'Some of the options have been changed. Are you sure to close the dialog?',
		ok				: 'OK',
		cancel			: 'Cancel',
		confirmationTitle	: 'Confirmation',
		messageTitle	: 'Information',
		inputTitle		: 'Question',
		undo			: 'Undo',
		redo			: 'Redo',
		skip			: 'Skip',
		skipAll			: 'Skip all',
		makeDecision	: 'What action should be taken?',
		rememberDecision: 'Remember my decision'
	},


	dir : 'ltr',
	HelpLang : 'en',
	LangCode : 'en',

	// Date Format
	//		d    : Day
	//		dd   : Day (padding zero)
	//		m    : Month
	//		mm   : Month (padding zero)
	//		yy   : Year (two digits)
	//		yyyy : Year (four digits)
	//		h    : Hour (12 hour clock)
	//		hh   : Hour (12 hour clock, padding zero)
	//		H    : Hour (24 hour clock)
	//		HH   : Hour (24 hour clock, padding zero)
	//		M    : Minute
	//		MM   : Minute (padding zero)
	//		a    : Firt char of AM/PM
	//		aa   : AM/PM
	DateTime : 'm/d/yyyy h:MM aa',
	DateAmPm : ['AM','PM'],

	// Folders
	FoldersTitle	: 'Thư mục',
	FolderLoading	: 'Đang tải...',
	FolderNew		: 'Vui lòng nhâp tên thư mục mới: ',
	FolderRename    : 'Vui lòng nhâp tên thư mục mới: ',
	FolderDelete	: 'Are you sure you want to delete the "%1" folder?',
	FolderRenaming	: ' (Đang xử lý...)',
	FolderDeleting  : ' (Đang xử lý...)',

	// Files
	FileRename      : 'Vui lòng nhâp tệp mới: ',
	FileRenameExt	: 'Are you sure you want to change the file name extension? The file may become unusable',
	FileRenaming	: 'Đang xử lý...',
	FileDelete		: 'Chắc chắn muốn xóa tệp tin "%1"?',
	FilesLoading	: 'Đang tải dữ liệu...',
	FilesEmpty      : 'Không có tập tin trong thư mục, hãy kéo và thả tập tin vào thư mục',
	FilesMoved		: 'File %1 moved into %2:%3',
	FilesCopied		: 'File %1 copied into %2:%3',

	// Basket
	BasketFolder		: 'Basket',
	BasketClear			: 'Clear Basket',
	BasketRemove		: 'Remove from basket',
	BasketOpenFolder	: 'Open parent folder',
	BasketTruncateConfirm : 'Do you really want to remove all files from the basket?',
	BasketRemoveConfirm	: 'Do you really want to remove the file "%1" from the basket?',
	BasketEmpty			: 'No files in the basket, drag\'n\'drop some.',
	BasketCopyFilesHere	: 'Copy Files from Basket',
	BasketMoveFilesHere	: 'Move Files from Basket',

	BasketPasteErrorOther	: 'File %s error: %e',
	BasketPasteMoveSuccess	: 'The following files were moved: %s',
	BasketPasteCopySuccess	: 'The following files were copied: %s',

	// Toolbar Buttons (some used elsewhere)
	Upload		: 'Tải lên',
	UploadTip	: 'Tải tệp mới',
	Refresh		: 'Làm mới',
	Settings	: 'Cài đặt',
	Help		: 'Trợ giúp',
	HelpTip     : 'Trợ giúp',

	// Context Menus
	Select			: 'Chọn',
	SelectThumbnail : 'Select Thumbnail',
	View			: 'Hiển thị',
	Download		: 'Tải về',

	NewSubFolder	: 'Tạo thư mục con',
	Rename			: 'Đổi tên',
	Delete			: 'Xóa',

	CopyDragDrop	: 'Copy file here',
	MoveDragDrop	: 'Move file here',

	// Dialogs
	RenameDlgTitle		: 'Đổi tên',
	NewNameDlgTitle		: 'Tên mới',
	FileExistsDlgTitle	: 'Tệp đã tồn tại',
	SysErrorDlgTitle : 'Lỗi hệ thống',

	FileOverwrite	: 'Ghi đè',
	FileAutorename	: 'Tự động đổi tên',

	// Generic
	OkBtn		: 'Chấp nhận',
	CancelBtn	: 'Hủy bỏ',
	CloseBtn	: 'Đóng',

	// Upload Panel
	UploadTitle			: 'Tải lên tệp mới',
	UploadSelectLbl		: 'Chọn tệp cần tải lên',
	UploadProgressLbl	: '(Đang xử lý, vui lòng chờ...)',
	UploadBtn			: 'Tải tệp đã chọn',
	UploadBtnCancel		: 'Hủy bỏ',

	UploadNoFileMsg		: 'Chọn 1 tệp tin từ máy tính của bạn',
	UploadNoFolder		: 'Chọn thư mục trước khi tải tệp tin.',
	UploadNoPerms       : 'Tập tin tải lên không được phép.',
	UploadUnknError		: 'Lỗi trong quá trình gửi tệp tin.',
	UploadExtIncorrect	: 'File extension not allowed in this folder.',

	// Settings Panel
	SetTitle		: 'Cài đặt lưới hiển thị',
	SetView			: 'Hiển thị theo:',
	SetViewThumb	: 'Mô tả',
	SetViewList		: 'Danh sách',
	SetDisplay		: 'Hiển thị:',
	SetDisplayName	: 'Tên tệp',
	SetDisplayDate	: 'Ngày tải',
	SetDisplaySize	: 'Độ lớn',
	SetSort			: 'Sắp xếp:',
	SetSortName		: 'Theo tên',
	SetSortDate		: 'Theo ngày tải',
	SetSortSize		: 'Theo độ lớn',

	// Status Bar
	FilesCountEmpty : '<Thư mục trống>',
	FilesCountOne	: '1 file',
	FilesCountMany	: '%1 files',

	// Size and Speed
	Kb				: '%1 kB',
	KbPerSecond		: '%1 kB/s',

	// Connector Error Messages.
	ErrorUnknown	: 'It was not possible to complete the request. (Error %1)',
	Errors :
	{
	 10 : 'Invalid command.',
	 11 : 'The resource type was not specified in the request.',
	 12 : 'The requested resource type is not valid.',
	102 : 'Invalid file or folder name.',
	103 : 'It was not possible to complete the request due to authorization restrictions.',
	104 : 'It was not possible to complete the request due to file system permission restrictions.',
	105 : 'Invalid file extension.',
	109 : 'Invalid request.',
	110 : 'Unknown error.',
	115 : 'A file or folder with the same name already exists.',
	116 : 'Folder not found. Please refresh and try again.',
	117 : 'File not found. Please refresh the files list and try again.',
	118 : 'Source and target paths are equal.',
	201 : 'A file with the same name is already available. The uploaded file has been renamed to "%1"',
	202 : 'Invalid file',
	203 : 'Invalid file. The file size is too big.',
	204 : 'The uploaded file is corrupt.',
	205 : 'No temporary folder is available for upload in the server.',
	206 : 'Upload cancelled for security reasons. The file contains HTML like data.',
	207 : 'The uploaded file has been renamed to "%1"',
	300 : 'Moving file(s) failed.',
	301 : 'Copying file(s) failed.',
	500 : 'The file browser is disabled for security reasons. Please contact your system administrator and check the CKFinder configuration file.',
	501 : 'The thumbnails support is disabled.'
	},

	// Other Error Messages.
	ErrorMsg :
	{
		FileEmpty		: 'The file name cannot be empty',
		FileExists		: 'File %s already exists',
		FolderEmpty		: 'The folder name cannot be empty',

		FileInvChar		: 'The file name cannot contain any of the following characters: \n\\ / : * ? " < > |',
		FolderInvChar	: 'The folder name cannot contain any of the following characters: \n\\ / : * ? " < > |',

		PopupBlockView	: 'It was not possible to open the file in a new window. Please configure your browser and disable all popup blockers for this site.'
	},

	// Imageresize plugin
	Imageresize :
	{
		dialogTitle		: 'Resize %s',
		sizeTooBig		: 'Cannot set image height or width to a value bigger than the original size (%size).',
		resizeSuccess	: 'Image resized successfully.',
		thumbnailNew	: 'Create new thumbnail',
		thumbnailSmall	: 'Nhỏ (%s)',
		thumbnailMedium	: 'Bình thường (%s)',
		thumbnailLarge	: 'Lớn (%s)',
		newSize			: 'Thiết lập kích thước mới',
		width			: 'Rộng',
		height			: 'Cao',
		invalidHeight	: 'Chiều cao không hợp lệ.',
		invalidWidth	: 'Chiều rộng không hợp lệ.',
		invalidName     : 'Tên tập tin không hợp lệ.',
		newImage		: 'Tạo ảnh mới',
		noExtensionChange : 'The file extension cannot be changed.',
		imageSmall		: 'Source image is too small',
		contextMenuName	: 'Resize'
	},

	// Fileeditor plugin
	Fileeditor :
	{
		save			: 'Lưu dữ liệu',
		fileOpenError   : 'Không thể mở tập tin.',
		fileSaveSuccess	: 'Lưu tệp tin thành công.',
		contextMenuName	: 'Sửa',
		loadingFile		: 'Đang tải tệp tin, vui lòng chờ...'
	}
};
