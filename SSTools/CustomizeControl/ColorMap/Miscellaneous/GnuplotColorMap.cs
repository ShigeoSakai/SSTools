﻿using System.Drawing;
namespace SSTools.ColorMap.Miscellaneous
{
	/// <summary>
	/// Gnuplotカラーマップ
	/// </summary>
	public class GnuplotColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GnuplotColorMap()
		{
			colorMap = gnuplot_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] gnuplot_map_ =
		{
			Color.FromArgb(0,0,0),		// 0
			Color.FromArgb(15,0,6),		// 1
			Color.FromArgb(22,0,12),		// 2
			Color.FromArgb(27,0,18),		// 3
			Color.FromArgb(31,0,25),		// 4
			Color.FromArgb(35,0,31),		// 5
			Color.FromArgb(39,0,37),		// 6
			Color.FromArgb(42,0,43),		// 7
			Color.FromArgb(45,0,49),		// 8
			Color.FromArgb(47,0,56),		// 9
			Color.FromArgb(50,0,62),		// 10
			Color.FromArgb(52,0,68),		// 11
			Color.FromArgb(55,0,74),		// 12
			Color.FromArgb(57,0,80),		// 13
			Color.FromArgb(59,0,86),		// 14
			Color.FromArgb(61,0,92),		// 15
			Color.FromArgb(63,0,97),		// 16
			Color.FromArgb(65,0,103),		// 17
			Color.FromArgb(67,0,109),		// 18
			Color.FromArgb(69,0,115),		// 19
			Color.FromArgb(71,0,120),		// 20
			Color.FromArgb(73,0,126),		// 21
			Color.FromArgb(74,0,131),		// 22
			Color.FromArgb(76,0,136),		// 23
			Color.FromArgb(78,0,142),		// 24
			Color.FromArgb(79,0,147),		// 25
			Color.FromArgb(81,0,152),		// 26
			Color.FromArgb(82,0,157),		// 27
			Color.FromArgb(84,0,162),		// 28
			Color.FromArgb(85,0,167),		// 29
			Color.FromArgb(87,0,171),		// 30
			Color.FromArgb(88,0,176),		// 31
			Color.FromArgb(90,0,180),		// 32
			Color.FromArgb(91,0,185),		// 33
			Color.FromArgb(93,0,189),		// 34
			Color.FromArgb(94,0,193),		// 35
			Color.FromArgb(95,0,197),		// 36
			Color.FromArgb(97,0,201),		// 37
			Color.FromArgb(98,0,205),		// 38
			Color.FromArgb(99,0,209),		// 39
			Color.FromArgb(100,0,212),		// 40
			Color.FromArgb(102,1,215),		// 41
			Color.FromArgb(103,1,219),		// 42
			Color.FromArgb(104,1,222),		// 43
			Color.FromArgb(105,1,225),		// 44
			Color.FromArgb(107,1,228),		// 45
			Color.FromArgb(108,1,230),		// 46
			Color.FromArgb(109,1,233),		// 47
			Color.FromArgb(110,1,236),		// 48
			Color.FromArgb(111,1,238),		// 49
			Color.FromArgb(112,1,240),		// 50
			Color.FromArgb(114,2,242),		// 51
			Color.FromArgb(115,2,244),		// 52
			Color.FromArgb(116,2,246),		// 53
			Color.FromArgb(117,2,247),		// 54
			Color.FromArgb(118,2,249),		// 55
			Color.FromArgb(119,2,250),		// 56
			Color.FromArgb(120,2,251),		// 57
			Color.FromArgb(121,3,252),		// 58
			Color.FromArgb(122,3,253),		// 59
			Color.FromArgb(123,3,253),		// 60
			Color.FromArgb(124,3,254),		// 61
			Color.FromArgb(125,3,254),		// 62
			Color.FromArgb(126,3,254),		// 63
			Color.FromArgb(127,4,254),		// 64
			Color.FromArgb(128,4,254),		// 65
			Color.FromArgb(129,4,254),		// 66
			Color.FromArgb(130,4,254),		// 67
			Color.FromArgb(131,4,253),		// 68
			Color.FromArgb(132,5,252),		// 69
			Color.FromArgb(133,5,251),		// 70
			Color.FromArgb(134,5,250),		// 71
			Color.FromArgb(135,5,249),		// 72
			Color.FromArgb(136,5,248),		// 73
			Color.FromArgb(137,6,246),		// 74
			Color.FromArgb(138,6,245),		// 75
			Color.FromArgb(139,6,243),		// 76
			Color.FromArgb(140,7,241),		// 77
			Color.FromArgb(141,7,239),		// 78
			Color.FromArgb(141,7,237),		// 79
			Color.FromArgb(142,7,234),		// 80
			Color.FromArgb(143,8,232),		// 81
			Color.FromArgb(144,8,229),		// 82
			Color.FromArgb(145,8,226),		// 83
			Color.FromArgb(146,9,223),		// 84
			Color.FromArgb(147,9,220),		// 85
			Color.FromArgb(148,9,217),		// 86
			Color.FromArgb(148,10,214),		// 87
			Color.FromArgb(149,10,210),		// 88
			Color.FromArgb(150,10,207),		// 89
			Color.FromArgb(151,11,203),		// 90
			Color.FromArgb(152,11,199),		// 91
			Color.FromArgb(153,11,195),		// 92
			Color.FromArgb(153,12,191),		// 93
			Color.FromArgb(154,12,187),		// 94
			Color.FromArgb(155,13,183),		// 95
			Color.FromArgb(156,13,178),		// 96
			Color.FromArgb(157,14,174),		// 97
			Color.FromArgb(158,14,169),		// 98
			Color.FromArgb(158,14,164),		// 99
			Color.FromArgb(159,15,159),		// 100
			Color.FromArgb(160,15,154),		// 101
			Color.FromArgb(161,16,149),		// 102
			Color.FromArgb(162,16,144),		// 103
			Color.FromArgb(162,17,139),		// 104
			Color.FromArgb(163,17,134),		// 105
			Color.FromArgb(164,18,128),		// 106
			Color.FromArgb(165,18,123),		// 107
			Color.FromArgb(165,19,117),		// 108
			Color.FromArgb(166,19,112),		// 109
			Color.FromArgb(167,20,106),		// 110
			Color.FromArgb(168,21,100),		// 111
			Color.FromArgb(168,21,95),		// 112
			Color.FromArgb(169,22,89),		// 113
			Color.FromArgb(170,22,83),		// 114
			Color.FromArgb(171,23,77),		// 115
			Color.FromArgb(171,24,71),		// 116
			Color.FromArgb(172,24,65),		// 117
			Color.FromArgb(173,25,59),		// 118
			Color.FromArgb(174,25,53),		// 119
			Color.FromArgb(174,26,46),		// 120
			Color.FromArgb(175,27,40),		// 121
			Color.FromArgb(176,27,34),		// 122
			Color.FromArgb(177,28,28),		// 123
			Color.FromArgb(177,29,21),		// 124
			Color.FromArgb(178,30,15),		// 125
			Color.FromArgb(179,30,9),		// 126
			Color.FromArgb(179,31,3),		// 127
			Color.FromArgb(180,32,0),		// 128
			Color.FromArgb(181,33,0),		// 129
			Color.FromArgb(182,33,0),		// 130
			Color.FromArgb(182,34,0),		// 131
			Color.FromArgb(183,35,0),		// 132
			Color.FromArgb(184,36,0),		// 133
			Color.FromArgb(184,37,0),		// 134
			Color.FromArgb(185,37,0),		// 135
			Color.FromArgb(186,38,0),		// 136
			Color.FromArgb(186,39,0),		// 137
			Color.FromArgb(187,40,0),		// 138
			Color.FromArgb(188,41,0),		// 139
			Color.FromArgb(188,42,0),		// 140
			Color.FromArgb(189,43,0),		// 141
			Color.FromArgb(190,44,0),		// 142
			Color.FromArgb(190,44,0),		// 143
			Color.FromArgb(191,45,0),		// 144
			Color.FromArgb(192,46,0),		// 145
			Color.FromArgb(192,47,0),		// 146
			Color.FromArgb(193,48,0),		// 147
			Color.FromArgb(194,49,0),		// 148
			Color.FromArgb(194,50,0),		// 149
			Color.FromArgb(195,51,0),		// 150
			Color.FromArgb(196,52,0),		// 151
			Color.FromArgb(196,54,0),		// 152
			Color.FromArgb(197,55,0),		// 153
			Color.FromArgb(198,56,0),		// 154
			Color.FromArgb(198,57,0),		// 155
			Color.FromArgb(199,58,0),		// 156
			Color.FromArgb(200,59,0),		// 157
			Color.FromArgb(200,60,0),		// 158
			Color.FromArgb(201,61,0),		// 159
			Color.FromArgb(201,62,0),		// 160
			Color.FromArgb(202,64,0),		// 161
			Color.FromArgb(203,65,0),		// 162
			Color.FromArgb(203,66,0),		// 163
			Color.FromArgb(204,67,0),		// 164
			Color.FromArgb(205,69,0),		// 165
			Color.FromArgb(205,70,0),		// 166
			Color.FromArgb(206,71,0),		// 167
			Color.FromArgb(206,72,0),		// 168
			Color.FromArgb(207,74,0),		// 169
			Color.FromArgb(208,75,0),		// 170
			Color.FromArgb(208,76,0),		// 171
			Color.FromArgb(209,78,0),		// 172
			Color.FromArgb(210,79,0),		// 173
			Color.FromArgb(210,81,0),		// 174
			Color.FromArgb(211,82,0),		// 175
			Color.FromArgb(211,83,0),		// 176
			Color.FromArgb(212,85,0),		// 177
			Color.FromArgb(213,86,0),		// 178
			Color.FromArgb(213,88,0),		// 179
			Color.FromArgb(214,89,0),		// 180
			Color.FromArgb(214,91,0),		// 181
			Color.FromArgb(215,92,0),		// 182
			Color.FromArgb(216,94,0),		// 183
			Color.FromArgb(216,95,0),		// 184
			Color.FromArgb(217,97,0),		// 185
			Color.FromArgb(217,98,0),		// 186
			Color.FromArgb(218,100,0),		// 187
			Color.FromArgb(218,102,0),		// 188
			Color.FromArgb(219,103,0),		// 189
			Color.FromArgb(220,105,0),		// 190
			Color.FromArgb(220,107,0),		// 191
			Color.FromArgb(221,108,0),		// 192
			Color.FromArgb(221,110,0),		// 193
			Color.FromArgb(222,112,0),		// 194
			Color.FromArgb(222,114,0),		// 195
			Color.FromArgb(223,115,0),		// 196
			Color.FromArgb(224,117,0),		// 197
			Color.FromArgb(224,119,0),		// 198
			Color.FromArgb(225,121,0),		// 199
			Color.FromArgb(225,123,0),		// 200
			Color.FromArgb(226,124,0),		// 201
			Color.FromArgb(226,126,0),		// 202
			Color.FromArgb(227,128,0),		// 203
			Color.FromArgb(228,130,0),		// 204
			Color.FromArgb(228,132,0),		// 205
			Color.FromArgb(229,134,0),		// 206
			Color.FromArgb(229,136,0),		// 207
			Color.FromArgb(230,138,0),		// 208
			Color.FromArgb(230,140,0),		// 209
			Color.FromArgb(231,142,0),		// 210
			Color.FromArgb(231,144,0),		// 211
			Color.FromArgb(232,146,0),		// 212
			Color.FromArgb(233,148,0),		// 213
			Color.FromArgb(233,150,0),		// 214
			Color.FromArgb(234,152,0),		// 215
			Color.FromArgb(234,154,0),		// 216
			Color.FromArgb(235,157,0),		// 217
			Color.FromArgb(235,159,0),		// 218
			Color.FromArgb(236,161,0),		// 219
			Color.FromArgb(236,163,0),		// 220
			Color.FromArgb(237,165,0),		// 221
			Color.FromArgb(237,168,0),		// 222
			Color.FromArgb(238,170,0),		// 223
			Color.FromArgb(238,172,0),		// 224
			Color.FromArgb(239,175,0),		// 225
			Color.FromArgb(240,177,0),		// 226
			Color.FromArgb(240,179,0),		// 227
			Color.FromArgb(241,182,0),		// 228
			Color.FromArgb(241,184,0),		// 229
			Color.FromArgb(242,187,0),		// 230
			Color.FromArgb(242,189,0),		// 231
			Color.FromArgb(243,192,0),		// 232
			Color.FromArgb(243,194,0),		// 233
			Color.FromArgb(244,197,0),		// 234
			Color.FromArgb(244,199,0),		// 235
			Color.FromArgb(245,202,0),		// 236
			Color.FromArgb(245,204,0),		// 237
			Color.FromArgb(246,207,0),		// 238
			Color.FromArgb(246,209,0),		// 239
			Color.FromArgb(247,212,0),		// 240
			Color.FromArgb(247,215,0),		// 241
			Color.FromArgb(248,217,0),		// 242
			Color.FromArgb(248,220,0),		// 243
			Color.FromArgb(249,223,0),		// 244
			Color.FromArgb(249,226,0),		// 245
			Color.FromArgb(250,228,0),		// 246
			Color.FromArgb(250,231,0),		// 247
			Color.FromArgb(251,234,0),		// 248
			Color.FromArgb(251,237,0),		// 249
			Color.FromArgb(252,240,0),		// 250
			Color.FromArgb(252,243,0),		// 251
			Color.FromArgb(253,246,0),		// 252
			Color.FromArgb(253,249,0),		// 253
			Color.FromArgb(254,252,0),		// 254
			Color.FromArgb(255,255,0),		// 255
		};
	}
}
