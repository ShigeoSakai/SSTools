﻿using System.Drawing;
namespace SSTools.ColorMap.Sequential2
{
	/// <summary>
	/// Grayカラーマップ
	/// </summary>
	public class GrayColorMap : ColorMapClass
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GrayColorMap()
		{
			colorMap = gray_map_;
		}
		/// <summary>
		/// カラーマップテーブル
		/// </summary>
		protected Color[] gray_map_ =
		{
			Color.FromArgb(0,0,0),		// 0
			Color.FromArgb(1,1,1),		// 1
			Color.FromArgb(2,2,2),		// 2
			Color.FromArgb(3,3,3),		// 3
			Color.FromArgb(4,4,4),		// 4
			Color.FromArgb(5,5,5),		// 5
			Color.FromArgb(6,6,6),		// 6
			Color.FromArgb(7,7,7),		// 7
			Color.FromArgb(8,8,8),		// 8
			Color.FromArgb(9,9,9),		// 9
			Color.FromArgb(10,10,10),		// 10
			Color.FromArgb(11,11,11),		// 11
			Color.FromArgb(12,12,12),		// 12
			Color.FromArgb(13,13,13),		// 13
			Color.FromArgb(14,14,14),		// 14
			Color.FromArgb(15,15,15),		// 15
			Color.FromArgb(16,16,16),		// 16
			Color.FromArgb(17,17,17),		// 17
			Color.FromArgb(18,18,18),		// 18
			Color.FromArgb(19,19,19),		// 19
			Color.FromArgb(20,20,20),		// 20
			Color.FromArgb(21,21,21),		// 21
			Color.FromArgb(22,22,22),		// 22
			Color.FromArgb(23,23,23),		// 23
			Color.FromArgb(24,24,24),		// 24
			Color.FromArgb(25,25,25),		// 25
			Color.FromArgb(26,26,26),		// 26
			Color.FromArgb(27,27,27),		// 27
			Color.FromArgb(28,28,28),		// 28
			Color.FromArgb(29,29,29),		// 29
			Color.FromArgb(30,30,30),		// 30
			Color.FromArgb(31,31,31),		// 31
			Color.FromArgb(32,32,32),		// 32
			Color.FromArgb(32,32,32),		// 33
			Color.FromArgb(34,34,34),		// 34
			Color.FromArgb(35,35,35),		// 35
			Color.FromArgb(36,36,36),		// 36
			Color.FromArgb(36,36,36),		// 37
			Color.FromArgb(38,38,38),		// 38
			Color.FromArgb(39,39,39),		// 39
			Color.FromArgb(40,40,40),		// 40
			Color.FromArgb(40,40,40),		// 41
			Color.FromArgb(42,42,42),		// 42
			Color.FromArgb(43,43,43),		// 43
			Color.FromArgb(44,44,44),		// 44
			Color.FromArgb(44,44,44),		// 45
			Color.FromArgb(46,46,46),		// 46
			Color.FromArgb(47,47,47),		// 47
			Color.FromArgb(48,48,48),		// 48
			Color.FromArgb(48,48,48),		// 49
			Color.FromArgb(50,50,50),		// 50
			Color.FromArgb(51,51,51),		// 51
			Color.FromArgb(52,52,52),		// 52
			Color.FromArgb(52,52,52),		// 53
			Color.FromArgb(54,54,54),		// 54
			Color.FromArgb(55,55,55),		// 55
			Color.FromArgb(56,56,56),		// 56
			Color.FromArgb(56,56,56),		// 57
			Color.FromArgb(58,58,58),		// 58
			Color.FromArgb(59,59,59),		// 59
			Color.FromArgb(60,60,60),		// 60
			Color.FromArgb(60,60,60),		// 61
			Color.FromArgb(62,62,62),		// 62
			Color.FromArgb(63,63,63),		// 63
			Color.FromArgb(64,64,64),		// 64
			Color.FromArgb(65,65,65),		// 65
			Color.FromArgb(65,65,65),		// 66
			Color.FromArgb(67,67,67),		// 67
			Color.FromArgb(68,68,68),		// 68
			Color.FromArgb(69,69,69),		// 69
			Color.FromArgb(70,70,70),		// 70
			Color.FromArgb(71,71,71),		// 71
			Color.FromArgb(72,72,72),		// 72
			Color.FromArgb(73,73,73),		// 73
			Color.FromArgb(73,73,73),		// 74
			Color.FromArgb(75,75,75),		// 75
			Color.FromArgb(76,76,76),		// 76
			Color.FromArgb(77,77,77),		// 77
			Color.FromArgb(78,78,78),		// 78
			Color.FromArgb(79,79,79),		// 79
			Color.FromArgb(80,80,80),		// 80
			Color.FromArgb(81,81,81),		// 81
			Color.FromArgb(81,81,81),		// 82
			Color.FromArgb(83,83,83),		// 83
			Color.FromArgb(84,84,84),		// 84
			Color.FromArgb(85,85,85),		// 85
			Color.FromArgb(86,86,86),		// 86
			Color.FromArgb(87,87,87),		// 87
			Color.FromArgb(88,88,88),		// 88
			Color.FromArgb(89,89,89),		// 89
			Color.FromArgb(89,89,89),		// 90
			Color.FromArgb(91,91,91),		// 91
			Color.FromArgb(92,92,92),		// 92
			Color.FromArgb(93,93,93),		// 93
			Color.FromArgb(94,94,94),		// 94
			Color.FromArgb(95,95,95),		// 95
			Color.FromArgb(96,96,96),		// 96
			Color.FromArgb(97,97,97),		// 97
			Color.FromArgb(97,97,97),		// 98
			Color.FromArgb(99,99,99),		// 99
			Color.FromArgb(100,100,100),		// 100
			Color.FromArgb(101,101,101),		// 101
			Color.FromArgb(102,102,102),		// 102
			Color.FromArgb(103,103,103),		// 103
			Color.FromArgb(104,104,104),		// 104
			Color.FromArgb(105,105,105),		// 105
			Color.FromArgb(105,105,105),		// 106
			Color.FromArgb(107,107,107),		// 107
			Color.FromArgb(108,108,108),		// 108
			Color.FromArgb(109,109,109),		// 109
			Color.FromArgb(110,110,110),		// 110
			Color.FromArgb(111,111,111),		// 111
			Color.FromArgb(112,112,112),		// 112
			Color.FromArgb(113,113,113),		// 113
			Color.FromArgb(113,113,113),		// 114
			Color.FromArgb(115,115,115),		// 115
			Color.FromArgb(116,116,116),		// 116
			Color.FromArgb(117,117,117),		// 117
			Color.FromArgb(118,118,118),		// 118
			Color.FromArgb(119,119,119),		// 119
			Color.FromArgb(120,120,120),		// 120
			Color.FromArgb(121,121,121),		// 121
			Color.FromArgb(121,121,121),		// 122
			Color.FromArgb(123,123,123),		// 123
			Color.FromArgb(124,124,124),		// 124
			Color.FromArgb(125,125,125),		// 125
			Color.FromArgb(126,126,126),		// 126
			Color.FromArgb(127,127,127),		// 127
			Color.FromArgb(128,128,128),		// 128
			Color.FromArgb(129,129,129),		// 129
			Color.FromArgb(130,130,130),		// 130
			Color.FromArgb(131,131,131),		// 131
			Color.FromArgb(131,131,131),		// 132
			Color.FromArgb(133,133,133),		// 133
			Color.FromArgb(134,134,134),		// 134
			Color.FromArgb(135,135,135),		// 135
			Color.FromArgb(136,136,136),		// 136
			Color.FromArgb(137,137,137),		// 137
			Color.FromArgb(138,138,138),		// 138
			Color.FromArgb(139,139,139),		// 139
			Color.FromArgb(140,140,140),		// 140
			Color.FromArgb(141,141,141),		// 141
			Color.FromArgb(142,142,142),		// 142
			Color.FromArgb(143,143,143),		// 143
			Color.FromArgb(144,144,144),		// 144
			Color.FromArgb(145,145,145),		// 145
			Color.FromArgb(146,146,146),		// 146
			Color.FromArgb(147,147,147),		// 147
			Color.FromArgb(147,147,147),		// 148
			Color.FromArgb(149,149,149),		// 149
			Color.FromArgb(150,150,150),		// 150
			Color.FromArgb(151,151,151),		// 151
			Color.FromArgb(152,152,152),		// 152
			Color.FromArgb(153,153,153),		// 153
			Color.FromArgb(154,154,154),		// 154
			Color.FromArgb(155,155,155),		// 155
			Color.FromArgb(156,156,156),		// 156
			Color.FromArgb(157,157,157),		// 157
			Color.FromArgb(158,158,158),		// 158
			Color.FromArgb(159,159,159),		// 159
			Color.FromArgb(160,160,160),		// 160
			Color.FromArgb(161,161,161),		// 161
			Color.FromArgb(162,162,162),		// 162
			Color.FromArgb(163,163,163),		// 163
			Color.FromArgb(163,163,163),		// 164
			Color.FromArgb(165,165,165),		// 165
			Color.FromArgb(166,166,166),		// 166
			Color.FromArgb(167,167,167),		// 167
			Color.FromArgb(168,168,168),		// 168
			Color.FromArgb(169,169,169),		// 169
			Color.FromArgb(170,170,170),		// 170
			Color.FromArgb(171,171,171),		// 171
			Color.FromArgb(172,172,172),		// 172
			Color.FromArgb(173,173,173),		// 173
			Color.FromArgb(174,174,174),		// 174
			Color.FromArgb(175,175,175),		// 175
			Color.FromArgb(176,176,176),		// 176
			Color.FromArgb(177,177,177),		// 177
			Color.FromArgb(178,178,178),		// 178
			Color.FromArgb(179,179,179),		// 179
			Color.FromArgb(179,179,179),		// 180
			Color.FromArgb(181,181,181),		// 181
			Color.FromArgb(182,182,182),		// 182
			Color.FromArgb(183,183,183),		// 183
			Color.FromArgb(184,184,184),		// 184
			Color.FromArgb(185,185,185),		// 185
			Color.FromArgb(186,186,186),		// 186
			Color.FromArgb(187,187,187),		// 187
			Color.FromArgb(188,188,188),		// 188
			Color.FromArgb(189,189,189),		// 189
			Color.FromArgb(190,190,190),		// 190
			Color.FromArgb(191,191,191),		// 191
			Color.FromArgb(192,192,192),		// 192
			Color.FromArgb(193,193,193),		// 193
			Color.FromArgb(194,194,194),		// 194
			Color.FromArgb(195,195,195),		// 195
			Color.FromArgb(195,195,195),		// 196
			Color.FromArgb(197,197,197),		// 197
			Color.FromArgb(198,198,198),		// 198
			Color.FromArgb(199,199,199),		// 199
			Color.FromArgb(200,200,200),		// 200
			Color.FromArgb(201,201,201),		// 201
			Color.FromArgb(202,202,202),		// 202
			Color.FromArgb(203,203,203),		// 203
			Color.FromArgb(204,204,204),		// 204
			Color.FromArgb(205,205,205),		// 205
			Color.FromArgb(206,206,206),		// 206
			Color.FromArgb(207,207,207),		// 207
			Color.FromArgb(208,208,208),		// 208
			Color.FromArgb(209,209,209),		// 209
			Color.FromArgb(210,210,210),		// 210
			Color.FromArgb(211,211,211),		// 211
			Color.FromArgb(211,211,211),		// 212
			Color.FromArgb(213,213,213),		// 213
			Color.FromArgb(214,214,214),		// 214
			Color.FromArgb(215,215,215),		// 215
			Color.FromArgb(216,216,216),		// 216
			Color.FromArgb(217,217,217),		// 217
			Color.FromArgb(218,218,218),		// 218
			Color.FromArgb(219,219,219),		// 219
			Color.FromArgb(220,220,220),		// 220
			Color.FromArgb(221,221,221),		// 221
			Color.FromArgb(222,222,222),		// 222
			Color.FromArgb(223,223,223),		// 223
			Color.FromArgb(224,224,224),		// 224
			Color.FromArgb(225,225,225),		// 225
			Color.FromArgb(226,226,226),		// 226
			Color.FromArgb(227,227,227),		// 227
			Color.FromArgb(227,227,227),		// 228
			Color.FromArgb(229,229,229),		// 229
			Color.FromArgb(230,230,230),		// 230
			Color.FromArgb(231,231,231),		// 231
			Color.FromArgb(232,232,232),		// 232
			Color.FromArgb(233,233,233),		// 233
			Color.FromArgb(234,234,234),		// 234
			Color.FromArgb(235,235,235),		// 235
			Color.FromArgb(236,236,236),		// 236
			Color.FromArgb(237,237,237),		// 237
			Color.FromArgb(238,238,238),		// 238
			Color.FromArgb(239,239,239),		// 239
			Color.FromArgb(240,240,240),		// 240
			Color.FromArgb(241,241,241),		// 241
			Color.FromArgb(242,242,242),		// 242
			Color.FromArgb(243,243,243),		// 243
			Color.FromArgb(243,243,243),		// 244
			Color.FromArgb(245,245,245),		// 245
			Color.FromArgb(246,246,246),		// 246
			Color.FromArgb(247,247,247),		// 247
			Color.FromArgb(248,248,248),		// 248
			Color.FromArgb(249,249,249),		// 249
			Color.FromArgb(250,250,250),		// 250
			Color.FromArgb(251,251,251),		// 251
			Color.FromArgb(252,252,252),		// 252
			Color.FromArgb(253,253,253),		// 253
			Color.FromArgb(254,254,254),		// 254
			Color.FromArgb(255,255,255),		// 255
		};
	}
}
